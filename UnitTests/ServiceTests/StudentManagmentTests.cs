using DatabaseLayer;
using DatabaseLayer.Entity;
using DatabaseLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.Common.Interfaces;
using ServiceLayer.DTO;
using ServiceLayer.DTO.Student;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Models;
using ServiceLayer.Services;
using ServiceLayer.Services.Student;
using System;
using System.Linq;
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
        private Mock<IConfiguration> _sharedConfigMock = new Mock<IConfiguration>();
        private Mock<IEmailSenderService> _emailMock = new Mock<IEmailSenderService>();
        private Mock<IRegistrationService> _regMock = new Mock<IRegistrationService>();
        private Mock<IErrorHandler> _handlerMock = new Mock<IErrorHandler>();
        private Mock<IDocumentService> _docMock = new Mock<IDocumentService>();
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
                .ReturnsAsync(new ResultMessage<RegistrationDTO>());

            Mock<IStudentService> studService = new Mock<IStudentService>();

            // Passing null here beacause GenerateRegistrationEmail does not use StudentService
            IStudentManagmentService studMng = new StudentManagmentService(
                _emailMock.Object,
                studService.Object,
                regMock.Object,
                _sharedConfigMock.Object,
                _docMock.Object);

            RegisterFormDTO reg = new RegisterFormDTO
            {
                Email = EMAIL_TO,
                FirstName = "FirstName",
                LastName = "LastName",
                Index = "1111/11",
                Privilege = StudentPrivilege.Normal
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
                .ReturnsAsync(new ResultMessage<RegistrationDTO>());

            _regMock.Setup(regService => regService.Update(It.IsAny<RegistrationDTO>()))
                .ReturnsAsync(new ResultMessage<RegistrationDTO>());

            var studServiceMock = new Mock<IStudentService>();
            studServiceMock.Setup(stud => stud.Add(It.IsAny<StudentDTO>()))
                .ReturnsAsync(new ResultMessage<StudentDTO>());

            IStudentManagmentService studMng = new StudentManagmentService
                (new Mock<IEmailSenderService>().Object,
                studServiceMock.Object,
                _regMock.Object,
                _sharedConfigMock.Object, null);

            RegisterFormDTO reg = new RegisterFormDTO
            {
                Email = EMAIL_TO,
                FirstName = "FirstName",
                LastName = "LastName",
                Index = "1111/11",
                Privilege = StudentPrivilege.Normal
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
                It.IsAny<Predicate<RegistrationDTO>>())).ReturnsAsync(new ResultMessage<RegistrationDTO>(regDTO));

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
            byte[] mockImgData = Enumerable.Range(0, 10)
                .Select(n => (byte)n)
                .ToArray();
            Mock<IUploadedFile> fileMock = new Mock<IUploadedFile>();
            fileMock
                .Setup(file => file.GetFileDataAsync())
                .ReturnsAsync(mockImgData);

            _docMock
                .Setup(doc => doc.UploadDocumentToStorage(It.IsAny<string>(), mockImgData))
                .ReturnsAsync(new ResultMessage<bool>(OperationStatus.Success));

            IServicesExecutor<StudentDTO, Student> executor
                = new ServiceExecutor<StudentDTO, Student>(_context, _handlerMock.Object);
            IStudentService studentService = new StudentService(executor);
            IStudentManagmentService studMngService
                = new StudentManagmentService(
                    _emailMock.Object,
                    studentService,
                    _regMock.Object,
                    _sharedConfigMock.Object,
                    _docMock.Object);

            Student stud = await _context.Students.FirstOrDefaultAsync();
            string newUsername = "New username";
            string newDescription = "New description";

            await studMngService.EditStudentProfile(stud.StudentId, new ProfileUpdateDTO
            {
                Username = "New username",
                Description = "New description",
                Photo = fileMock.Object
            });

            stud = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == stud.StudentId);
            Assert.AreEqual(newUsername, stud.Username);
            Assert.AreEqual(newDescription, stud.Description);
            _docMock.Verify(doc => doc.UploadDocumentToStorage(It.IsAny<string>(), mockImgData), Times.Once);

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
                    _sharedConfigMock.Object,
                    _docMock.Object);

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
