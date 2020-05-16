using DatabaseLayer.Enums;
using Microsoft.Extensions.Configuration;
using ServiceLayer.AutoMapper;
using ServiceLayer.Common.Interfaces;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.ErrorHandling.Exceptions;
using ServiceLayer.Helpers;
using ServiceLayer.Models;
using System;
using System.Threading.Tasks;
using ServiceLayer.DTO.Student;
using ServiceLayer.Shared;

namespace ServiceLayer.Services.Student
{
    public class StudentManagmentService : IStudentManagmentService
    {


        private readonly IEmailSenderService _emailSender;
        private readonly IStudentService _studentService;
        private readonly IRegistrationService _registrationService;
        private readonly IDocumentService _docService;
        private readonly Uri _baseUrl;
        private readonly string _fromEmailAddress;
        private readonly string _emailSubject;
        private readonly string _emailLinkEndpoint;

        // TODO: Replace this with text/html once we design looks of registration email
        private const string CONTENT_TYPE = "text/plain";
        private const int REGISTRATION_CODE_SIZE = 16; // In bytes

        public StudentManagmentService(
            IEmailSenderService emailSender,
            IStudentService studentService,
            IRegistrationService registrationService,
            IConfiguration config,
            IDocumentService docService)
        {
            (_emailSender, _studentService, _registrationService, _docService) =
                (emailSender, studentService, registrationService, docService);
            (_baseUrl, _fromEmailAddress, _emailSubject) =
                (new Uri(config["BaseUrl"]), config["RegisterEmail:From"], config["RegisterEmail:Subject"]);
            _emailLinkEndpoint = config["RegisterEmail:LinkEndpoint"];
        }

        public async Task<ResultMessage<bool>> GenerateRegistrationEmail(RegisterFormDTO registerFormDTO)
        {
            RegistrationDTO regDTO = Mapping.Mapper.Map<RegistrationDTO>(registerFormDTO);
            regDTO.RegistrationCode = GenerateRegistrationCode();
            regDTO.Used = false;
            regDTO.Timestamp = DateTime.Now;

            ResultMessage<RegistrationDTO> registrationProcessResult = await _registrationService.Add(regDTO);
            if (!registrationProcessResult.IsSuccess)
                return new ResultMessage<bool>(registrationProcessResult.Status, registrationProcessResult.Message);

            await _emailSender.SendEmailAsync(
                new SendEmailData
                {
                    From = _fromEmailAddress,
                    To = registerFormDTO.Email,
                    ContentType = CONTENT_TYPE,
                    Message = GenerateRegistrationLink(regDTO.RegistrationCode),
                    Subject = _emailSubject
                });
            return new ResultMessage<bool>(true);
        }

        private string GenerateRegistrationLink(string code)
            => new Uri(_baseUrl, $"{_emailLinkEndpoint}?code={Uri.EscapeDataString(code)}").AbsoluteUri;

        public async Task<bool> IsStudentRegistered(string email)
            => (await _studentService.GetSingleOrDefault(student => student.Email == email)).Result != null;


        private string GenerateRegistrationCode()
            => Convert.ToBase64String(Crypto.GenerateRandomBytes(REGISTRATION_CODE_SIZE));

        /// <summary>
        /// Checks whether registration code is valid
        /// </summary>
        /// <param name="code">Registration link code</param>
        /// <returns>Task which wraps result</returns>
        /// <remarks>Code is valid if registration for given code exists 
        /// and user with given email is not already registered</remarks>
        public async Task<bool> ValidateRegistrationCode(string code)
        {
            RegistrationDTO registration =
                await _registrationService.GetSingleOrDefault(reg => reg.RegistrationCode == code && !reg.Used);

            return registration != null && !await IsStudentRegistered(registration.Email);
        }

        public async Task<ResultMessage<RegistrationDTO>> RegisterStudent(RegisterUserDTO registerData)
        {

            StudentDTO student = Mapping.Mapper.Map<StudentDTO>(registerData);

            ResultMessage<RegistrationDTO> res = await _registrationService.GetSingleOrDefault(
                reg => reg.RegistrationCode == registerData.RegistrationCode);

            RegistrationDTO registration = res.Result;
            Mapping.Mapper.Map(registration, student);

            byte[] salt = Crypto.GenerateRandomBytes(Constants.SALT_SIZE);
            string saltBase64 = Convert.ToBase64String(salt);
            string hash = Crypto.CreateHash(registerData.Password, salt,
                Constants.PASSWORD_HASH_SIZE);
            student.PasswordHash = hash;
            student.Salt = saltBase64;
            student.Privilege = StudentPrivilege.Normal;
            student.Rating = 0;
            registration.Used = true;

            ResultMessage<RegistrationDTO> registrationProcessResult = await _registrationService.Update(registration);
            if (!registrationProcessResult.IsSuccess)
                return new ResultMessage<RegistrationDTO>(registrationProcessResult.Status, "Registration couldn't been updated." + registrationProcessResult.Message);
            ResultMessage<StudentDTO> dtoStudent = await _studentService.Add(student);
            if (!dtoStudent.IsSuccess)
                return new ResultMessage<RegistrationDTO>(dtoStudent.Status, "Registration couldn't been updated." + dtoStudent.Message);

            return registrationProcessResult;
        }

        public async Task RegisterRootUser(RegisterRootDTO rootData)
        {
            StudentDTO studentDTO = Mapping.Mapper.Map<StudentDTO>(rootData);
            byte[] salt = Crypto.GenerateRandomBytes(Constants.SALT_SIZE);
            string saltBase64 = Convert.ToBase64String(salt);
            string hash = Crypto.CreateHash(rootData.Password, salt,
                Constants.PASSWORD_HASH_SIZE);
            studentDTO.PasswordHash = hash;
            studentDTO.Salt = saltBase64;
            studentDTO.Privilege = StudentPrivilege.SeniorAdmin;
            studentDTO.Rating = 0;

            ResultMessage<StudentDTO> studentResult = await _studentService.Add(studentDTO);
            if (!studentResult.IsSuccess)
                throw new CouldNotRegisterRootUserException(studentResult.Status.ToString());
        }

        public async Task<ResultMessage<StudentDTO>> EditStudentProfile(int id, ProfileUpdateDTO profile)
        {
            StudentDTO stud = await _studentService.GetSingleOrDefault(s => s.StudentId == id);

            if (profile.Photo != null)
            {
                string path = PathBuilder.BuildPathForProfilePictures(id);
                await _docService.UploadDocumentToStorage(path, await profile.Photo.GetFileDataAsync());
                stud.Photo = path;
            }

            stud.Username = profile.Username;
            if(! string.IsNullOrEmpty(profile.Description))
                stud.Description = profile.Description;

            return await _studentService.Update(stud);
        }

        public async Task<PassChangeStatus> UpdateStudentPassword(int id, UpdatePasswordDTO passwordDTO)
        {
            StudentDTO student
                = await _studentService.GetSingleOrDefault(s => s.StudentId == id);

            bool verifyPass = VerifyOldPassword(
                passwordDTO.OldPassword,
                student.Salt,
                student.PasswordHash);

            if (!verifyPass)
                return PassChangeStatus.InvalidOldPass;
            else if (passwordDTO.NewPassword != passwordDTO.ConfirmPassword)
                return PassChangeStatus.PassNoMatch;

            byte[] salt = Crypto.GenerateRandomBytes(Constants.SALT_SIZE);
            string saltBase64 = Convert.ToBase64String(salt);
            student.Salt = saltBase64;
            student.PasswordHash = Crypto.CreateHash(
                passwordDTO.NewPassword,
                salt,
                Constants.PASSWORD_HASH_SIZE);

            ResultMessage<StudentDTO> updatedStudent = await _studentService.Update(student);
            if (!updatedStudent.IsSuccess)
                return PassChangeStatus.Error;
            return PassChangeStatus.Success;
        }

        private bool VerifyOldPassword(string oldPassword, string salt, string oldPasswordHash)
            => Crypto.CreateHash(oldPassword,
                Convert.FromBase64String(salt),
                Constants.PASSWORD_HASH_SIZE) == oldPasswordHash;

        public async Task<bool> ValidatePassword(int id, string password)
        {
            StudentDTO st = await _studentService.GetSingleOrDefault(s => s.StudentId == id);
            return VerifyOldPassword(password, st.Salt, st.PasswordHash);
        }
    }
}
