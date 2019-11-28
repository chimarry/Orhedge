using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.Common.Interfaces;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Models;
using ServiceLayer.Students.Interfaces;
using ServiceLayer.Students.Services;
using System;
using System.Threading.Tasks;

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
            await studMng.GenerateRegistrationEmail(EMAIL_TO);

            emailMock.Verify(emailService => emailService.SendEmailAsync
            (
                It.Is<SendEmailData>(emailData => emailData.To == EMAIL_TO && emailData.Subject == EMAIL_SUBJECT)
            ), Times.Once);

            regMock.Verify(regService => regService.Add(
                It.Is<RegistrationDTO>(regDTO => regDTO.Email == EMAIL_TO)), Times.Once);
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
            await studMng.GenerateRegistrationEmail(EMAIL_TO);
            
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

            RegisterData regData = new RegisterData
            {
                FirstName = "Name",
                LastName = "LastName",
                Password = "Password",
                ConfirmPassword = "Password",
                Index = "1111/19",
                Username = "username",
                RegistrationCode = registrationCode
            };

            await studMng.RegisterStudent(regData);

            studServiceMock.Verify(service => service.Add(
                It.Is<StudentDTO>(dto => dto.Email == EMAIL_TO
                && dto.Privilege == 3 && dto.Rating == 0
                && dto.Username == regData.Username && !dto.Deleted && 
                dto.Index == regData.Index && dto.Name == regData.FirstName 
                && dto.LastName == regData.LastName)));

            regMock.Verify(service => service.Update(
                It.Is<RegistrationDTO>(dto => dto.Used == true)));
        }
    }
}
