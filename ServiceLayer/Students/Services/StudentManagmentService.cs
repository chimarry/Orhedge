using Microsoft.Extensions.Configuration;
using ServiceLayer.AutoMapper;
using ServiceLayer.Common;
using ServiceLayer.Common.Interfaces;
using ServiceLayer.DTO;
using ServiceLayer.Models;
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

        public async Task GenerateRegistrationEmail(string email)
        {
            RegistrationDTO regDTO = new RegistrationDTO
            {
                Email = email,
                RegistrationCode = GenerateRegistrationCode(),
                Timestamp = DateTime.Now,
                Used = false
            };

            await _registrationService.Add(regDTO);

            await _emailSender.SendEmailAsync(
                new SendEmailData
                {
                    From = _fromEmailAddress,
                    To = email,
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

        // Make sure to check if registerData.Password == registerData.ConfirmPassword
        public async Task RegisterStudent(RegisterData registerData)
        {

            if (registerData.Password != registerData.ConfirmPassword)
                throw new ArgumentException($"{nameof(registerData.Password)} " +
                    $"and {registerData.ConfirmPassword} properties are not equal", nameof(registerData));

            StudentDTO student = Mapping.Mapper.Map<StudentDTO>(registerData);

            RegistrationDTO registration = await _registrationService.GetSingleOrDefault(
                reg => reg.RegistrationCode == registerData.RegistrationCode);

            student.Email = registration.Email;
            student.Privilege = 3;
            student.Rating = 0;
            byte[] salt = Crypto.GenerateRandomBytes(SALT_SIZE);
            string saltBase64 = Convert.ToBase64String(salt);
            string hash = Convert.ToBase64String(Crypto.DeriveKey(registerData.Password, salt, 
                Constants.PASSWORD_HASH_SIZE));
            student.PasswordHash = hash;
            student.Salt = saltBase64;
            registration.Used = true;

            await _registrationService.Update(registration);
            await _studentService.Add(student);
        }
    }
}
