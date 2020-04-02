﻿using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.Common.Interfaces;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Services;
using ServiceLayer.Models;
using System;
using System.Threading.Tasks;
using UnitTests.Common;
using ServiceLayer.Helpers;
using DatabaseLayer.Entity;
using DatabaseLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.DTO.Student;
using ServiceLayer.Services.Student;

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
        private Mock<IConfiguration> _sharedConfigMock = new Mock<IConfiguration>();
        private Mock<IEmailSenderService> _emailMock = new Mock<IEmailSenderService>();
        private Mock<IRegistrationService> _regMock = new Mock<IRegistrationService>();
        private Mock<IErrorHandler> _handlerMock = new Mock<IErrorHandler>();
        private OrhedgeContext _context;

        [TestInitialize]
        public void Initialize()
        {
            var configMock = new Mock<IConfiguration>();
            _sharedConfigMock.SetupGet(config => config["BaseUrl"]).Returns(EMAIL_LINK_BASE_URL);
            _sharedConfigMock.SetupGet(config => config["RegisterEmail:From"])
                .Returns(EMAIL_FROM);
            _sharedConfigMock.SetupGet(config => config["RegisterEmail:Subject"])
                .Returns(EMAIL_SUBJECT);
            _sharedConfigMock.SetupGet(config => config["RegisterEmail:LinkEndpoint"])
                .Returns(EMAIL_LINK_ENDPOINT);
            _context = Utilities.CreateNewContext();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }


        [TestMethod]
        public async Task GenerateRegistrationEmail()
        {

            _emailMock.Setup(emailService => emailService.SendEmailAsync(It.IsAny<SendEmailData>()))
                .Returns(Task.CompletedTask);

            var regMock = new Mock<IRegistrationService>();
            regMock.Setup(regService => regService.Add(It.IsAny<RegistrationDTO>()))
                .ReturnsAsync(Status.SUCCESS);

            Mock<IStudentService> studService = new Mock<IStudentService>();

            // Passing null here beacause GenerateRegistrationEmail does not use StudentService
            IStudentManagmentService studMng = new StudentManagmentService(
                _emailMock.Object, studService.Object, regMock.Object, _sharedConfigMock.Object);

            RegisterFormDTO reg = new RegisterFormDTO
            {
                Email = EMAIL_TO,
                FirstName = "FirstName",
                LastName = "LastName",
                Index = "1111/11",
                Privilege = 3
            };

            await studMng.GenerateRegistrationEmail(reg);

            _emailMock.Verify(emailService => emailService.SendEmailAsync
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

            _regMock.Setup(regService => regService.Add(It.IsAny<RegistrationDTO>()))
                // Note callback here setting registrationCode variable
                .Callback((RegistrationDTO dto) => registrationCode = dto.RegistrationCode)
                .Returns(Task.FromResult(Status.SUCCESS));

            var studServiceMock = new Mock<IStudentService>();

            IStudentManagmentService studMng = new StudentManagmentService
                (new Mock<IEmailSenderService>().Object, 
                studServiceMock.Object,
                _regMock.Object, 
                _sharedConfigMock.Object);

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

            _regMock.Setup(regService => regService.GetSingleOrDefault(
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

            _regMock.Verify(service => service.Update(
                It.Is<RegistrationDTO>(dto => dto.Used == true)));
        }

        [TestMethod]
        public async Task UpdateStudentProfile()
        {

            IServicesExecutor<StudentDTO, Student> executor
                = new ServiceExecutor<StudentDTO, Student>(_context, _handlerMock.Object);
            IStudentService studentService = new StudentService(executor);
            IStudentManagmentService studMngService
                = new StudentManagmentService(
                    _emailMock.Object, 
                    studentService, 
                    _regMock.Object, 
                    _sharedConfigMock.Object);

            Student stud = await _context.Students.FirstOrDefaultAsync();
            string newUsername = "New username";
            string newDescription = "New description";

            await studMngService.EditStudentProfile(stud.StudentId, new ProfileUpdateDTO()
            {
                Username = "New username",
                Description = "New description"
                // TODO: Add Photo
            });

            stud = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == stud.StudentId);
            Assert.AreEqual(newUsername, stud.Username);
            Assert.AreEqual(newDescription, stud.Description);

        }

        [DataTestMethod]
        [DataRow("lightPassword", "newPassword", "newPassword")]
        [DataRow("lightPassword", "newPassword", "NEWPASSWORD")]
        [DataRow("LIGHTPASSWORD", "newPassword", "newPassword")]
        public async Task ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            Mock<IEmailSenderService> mockEmail = new Mock<IEmailSenderService>();
            Mock<IRegistrationService> mockReg = new Mock<IRegistrationService>();
            Mock<IErrorHandler> handlerMock = new Mock<IErrorHandler>();

            IServicesExecutor<StudentDTO, Student> executor
                = new ServiceExecutor<StudentDTO, Student>(_context, handlerMock.Object);
            IStudentService studentService = new StudentService(executor);
            IStudentManagmentService studMngService
                = new StudentManagmentService(
                    mockEmail.Object, 
                    studentService, 
                    mockReg.Object, 
                    _sharedConfigMock.Object);

            Student stud = await _context.Students.FirstOrDefaultAsync(s => s.Username == "light");
            const string studOldPassword = "lightPassword";

            PassChangeStatus status = await studMngService.UpdateStudentPassword(
                stud.StudentId, new UpdatePasswordDTO
            {
                OldPassword = oldPassword,
                NewPassword = newPassword,
                ConfirmPassword = confirmPassword
            });

            if (studOldPassword != oldPassword)
                Assert.AreEqual(PassChangeStatus.InvalidOldPass, status);
            else if (newPassword != confirmPassword)
                Assert.AreEqual(PassChangeStatus.PassNoMatch, status);
            else
                Assert.AreEqual(PassChangeStatus.Success, status);
        }
    }
}
