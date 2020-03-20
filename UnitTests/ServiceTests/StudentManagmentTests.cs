using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.Common.Interfaces;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Services;
using ServiceLayer.Models;
using ServiceLayer.Services;
using System;
using System.Threading.Tasks;
using UnitTests.Common;

namespace UnitTests.ServiceTests
{
    [TestClass]
    public class StudentManagmentTests
    {

        private const string EMAIL_TO = "test@test.com";
        private const string EMAIL_FROM = "noreply@orhedge.com";
        private const string EMAIL_LINK_BASE_URL = "https://test.com";
        private const string EMAIL_SUBJECT = "Registration email";
        private const string EMAIL_LINK_ENDPOINT = "Register/ShowRegisterForm";
        private readonly Mock<IConfiguration> _sharedConfigMock;

        public StudentManagmentTests()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.SetupGet(config => config["BaseUrl"]).Returns(EMAIL_LINK_BASE_URL);
            configMock.SetupGet(config => config["RegisterEmail:From"])
                .Returns(EMAIL_FROM);
            configMock.SetupGet(config => config["RegisterEmail:Subject"])
                .Returns(EMAIL_SUBJECT);
            configMock.SetupGet(config => config["RegisterEmail:LinkEndpoint"])
                .Returns(EMAIL_LINK_ENDPOINT);
            _sharedConfigMock = configMock;
        }


        [TestMethod]
        public async Task GenerateRegistrationEmail()
        {

            var emailMock = new Mock<IEmailSenderService>();
            emailMock.Setup(emailService => emailService.SendEmailAsync(It.IsAny<SendEmailData>()))
                .Returns(Task.CompletedTask);

            var regMock = new Mock<IRegistrationService>();
            regMock.Setup(regService => regService.Add(It.IsAny<RegistrationDTO>()))
                .ReturnsAsync(Status.SUCCESS);

            // Passing null here beacause GenerateRegistrationEmail does not use StudentService
            IStudentManagmentService studMng = new StudentManagmentService(
                emailMock.Object, null, regMock.Object, _sharedConfigMock.Object);

            RegisterFormDTO reg = new RegisterFormDTO
            {
                Email = EMAIL_TO,
                FirstName = "FirstName",
                LastName = "LastName",
                Index = "1111/11",
                Privilege = 3
            };

            await studMng.GenerateRegistrationEmail(reg);

            emailMock.Verify(emailService => emailService.SendEmailAsync
            (
                It.Is<SendEmailData>(emailData => emailData.To == EMAIL_TO && emailData.Subject == EMAIL_SUBJECT)
            ), Times.Once);

            regMock.Verify(regService => regService.Add(
                It.Is<RegistrationDTO>(regDTO => regDTO.Compare(reg) && regDTO.Used == false)), Times.Once);
        }

        [TestMethod]
        public async Task RegisterStudent()
        {

            string registrationCode = null;


            var regMock = new Mock<IRegistrationService>();
            regMock.Setup(regService => regService.Add(It.IsAny<RegistrationDTO>()))
                // Note callback here setting registrationCode variable
                .Callback((RegistrationDTO dto) => registrationCode = dto.RegistrationCode)
                .Returns(Task.FromResult(Status.SUCCESS));

            var studServiceMock = new Mock<IStudentService>();

            IStudentManagmentService studMng = new StudentManagmentService
                (new Mock<IEmailSenderService>().Object, studServiceMock.Object, regMock.Object, _sharedConfigMock.Object);

            RegisterFormDTO reg = new RegisterFormDTO
            {
                Email = EMAIL_TO,
                FirstName = "FirstName",
                LastName = "LastName",
                Index = "1111/11",
                Privilege = 3
            };

            await studMng.GenerateRegistrationEmail(reg);

            // At this point registrationCode != null 
            RegistrationDTO regDTO = new RegistrationDTO
            {
                Email = EMAIL_TO,
                RegistrationCode = registrationCode,
                RegistrationId = 1,
                Timestamp = DateTime.UtcNow,
                Used = false
            };

            regMock.Setup(regService => regService.GetSingleOrDefault(
                It.IsAny<Predicate<RegistrationDTO>>())).ReturnsAsync(regDTO);

            RegisterUserDTO regData = new RegisterUserDTO
            {
                Password = "Password",
                Username = "username",
                RegistrationCode = registrationCode
            };

            await studMng.RegisterStudent(regData);

            studServiceMock.Verify(service => service.Add(
                It.Is<StudentDTO>(dto => dto.Username == regData.Username)));

            regMock.Verify(service => service.Update(
                It.Is<RegistrationDTO>(dto => dto.Used == true)));
        }
    }
}
