using Microsoft.Extensions.Configuration;
using ServiceLayer.AutoMapper;
using ServiceLayer.Common;
using ServiceLayer.Common.Interfaces;
using ServiceLayer.DTO;
using ServiceLayer.DTO.Registration;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Models;
using ServiceLayer.Students.Exceptions;
using ServiceLayer.Students.Interfaces;
using ServiceLayer.Utilities;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class StudentManagmentService : IStudentManagmentService
    {


        private readonly IEmailSenderService _emailSender;
        private readonly IStudentService _studentService;
        private readonly IRegistrationService _registrationService;
        private readonly Uri _baseUrl;
        private string _fromEmailAddress;
        private string _emailSubject;
        private string _emailLinkEndpoint; 

        // TODO: Replace this with text/html once we design looks of registration email
        private const string CONTENT_TYPE = "text/plain";
        private const int REGISTRATION_CODE_SIZE = 16; // In bytes
        private const int SALT_SIZE = 16;

        public StudentManagmentService(
            IEmailSenderService emailSender, 
            IStudentService studentService,
            IRegistrationService registrationService,
            IConfiguration config)
        {
            (_emailSender, _studentService, _registrationService) = 
                (emailSender, studentService, registrationService);
            (_baseUrl, _fromEmailAddress, _emailSubject) = 
                (new Uri(config["BaseUrl"]), config["RegisterEmail:From"], config["RegisterEmail:Subject"]);
            _emailLinkEndpoint = config["RegisterEmail:LinkEndpoint"];
        }

        public async Task GenerateRegistrationEmail(RegisterFormDTO registerFormDTO)
        {

            RegistrationDTO regDTO = Mapping.Mapper.Map<RegistrationDTO>(registerFormDTO);
            regDTO.RegistrationCode = GenerateRegistrationCode();
            regDTO.Used = false;
            regDTO.Timestamp = DateTime.Now;

            await _registrationService.Add(regDTO);

            await _emailSender.SendEmailAsync(
                new SendEmailData
                {
                    From = _fromEmailAddress,
                    To = registerFormDTO.Email,
                    ContentType = CONTENT_TYPE,
                    Message = GenerateRegistrationLink(regDTO.RegistrationCode),
                    Subject = _emailSubject
                });
        }

        private string GenerateRegistrationLink(string code)
            => new Uri(_baseUrl, $"{_emailLinkEndpoint}?code={Uri.EscapeDataString(code)}").AbsoluteUri;
        
        public async Task<bool> IsStudentRegistered(string email)
            => await _studentService.GetSingleOrDefault(student => student.Email == email) != null;


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

        public async Task RegisterStudent(RegisterUserDTO registerData)
        {

            StudentDTO student = Mapping.Mapper.Map<StudentDTO>(registerData);

            RegistrationDTO registration = await _registrationService.GetSingleOrDefault(
                reg => reg.RegistrationCode == registerData.RegistrationCode);

            Mapping.Mapper.Map(registration, student);

            byte[] salt = Crypto.GenerateRandomBytes(SALT_SIZE);
            string saltBase64 = Convert.ToBase64String(salt);
            string hash = Crypto.CreateHash(registerData.Password, salt, 
                Constants.PASSWORD_HASH_SIZE);
            student.PasswordHash = hash;
            student.Salt = saltBase64;
            // TODO: Replace with enum value
            student.Privilege = 2;
            student.Rating = 0;
            registration.Used = true;

            await _registrationService.Update(registration);
            await _studentService.Add(student);
        }

        public async Task RegisterRootUser(RegisterRootDTO rootData)
        {
            StudentDTO studentDTO = Mapping.Mapper.Map<StudentDTO>(rootData);
            byte[] salt = Crypto.GenerateRandomBytes(SALT_SIZE);
            string saltBase64 = Convert.ToBase64String(salt);
            string hash = Crypto.CreateHash(rootData.Password, salt, 
                Constants.PASSWORD_HASH_SIZE);
            studentDTO.PasswordHash = hash;
            studentDTO.Salt = saltBase64;
            // TODO: Replace with enum value
            studentDTO.Privilege = 4;
            studentDTO.Rating = 0;

            // TODO: Ideally this method should throw, we can not handle it in any meanigful way anyway
            if (await _studentService.Add(studentDTO) == Status.DATABASE_ERROR)
                throw new DatabaseErrorException();
        }
    }
}
