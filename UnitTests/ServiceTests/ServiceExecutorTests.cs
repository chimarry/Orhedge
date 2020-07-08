using DatabaseLayer;
using DatabaseLayer.Entity;
using DatabaseLayer.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTests.Common;

namespace UnitTests.ServiceTests
{
    [TestClass]
    public class ServiceExecutorTests
    {

        [TestMethod]
        public async Task GetSingleOrDefault()
        {

            using (OrhedgeContext context = Utilities.CreateNewContext())
            {
                var errHandlerMock = new Mock<IErrorHandler>();
                var executor = new ServiceExecutor<StudentDTO, Student>(context, errHandlerMock.Object);
                List<StudentDTO> students = await executor.GetAll<int>(dtoCondition: s => true);
                StudentDTO student = await executor.GetSingleOrDefault((StudentDTO x) => x.Username == "light");
                Assert.AreNotEqual(student, null);

                StudentDTO noStudent = await executor.GetSingleOrDefault((StudentDTO x) => x.Username == "nonexistent");
                Assert.AreEqual(noStudent, null);

                // Make sure ErrorHandler.Handle method was not called
                errHandlerMock.Verify(err => err.Log(It.IsAny<Exception>()), Times.Never);
            }
        }

        [TestMethod]
        public async Task Add()
        {
            StudentDTO correctStudent = new StudentDTO()
            {
                Index = "1109/18",
                Name = "Ilija",
                LastName = "Ilic",
                Email = "ilija.ilic@gmail.com",
                Privilege = 0,
                PasswordHash = "sa99d...10043",
                Username = "obscure",
                Salt = "90"
            };

            using (OrhedgeContext context = Utilities.CreateNewContext())
            {
                var errHandlerMock = new Mock<IErrorHandler>();
                var executor = new ServiceExecutor<StudentDTO, Student>(context, errHandlerMock.Object);
                ResultMessage<StudentDTO> addedStudentResult = await executor.Add(correctStudent, s => s.Email == correctStudent.Email
                                               && s.Index == correctStudent.Index && s.Username == correctStudent.Username);

                // Check status of operation
                Assert.AreEqual(addedStudentResult.Status, OperationStatus.Success);

                // Check if inserted record exists with valid data
                StudentDTO student = await executor.GetSingleOrDefault((StudentDTO x) => x.Email == correctStudent.Email);
                Assert.IsNotNull(student);
                Assert.AreEqual(student.Index, correctStudent.Index, "Wrong index");
                Assert.AreEqual(student.Username, correctStudent.Username, "Wrong username");
                Assert.AreEqual(student.Privilege, correctStudent.Privilege, "Wrong privlege");
                Assert.AreEqual(student.Name, correctStudent.Name, "Wrong name");
                Assert.AreEqual(student.LastName, correctStudent.LastName, "Wrong last name");

                // Make sure ErrorHandler.Handle method was not called
                errHandlerMock.Verify(err => err.Log(It.IsAny<Exception>()), Times.Never);
            }
        }

        [TestMethod]
        public async Task Update()
        {

            using (OrhedgeContext context = Utilities.CreateNewContext())
            {
                var errHandlerMock = new Mock<IErrorHandler>();
                var executor = new ServiceExecutor<StudentDTO, Student>(context, errHandlerMock.Object);
                StudentDTO correctUpdatedStudent = await executor.GetSingleOrDefault((StudentDTO x) => x.Username == "light");
                correctUpdatedStudent.Name = "Milica";
                correctUpdatedStudent.Rating = 3;
                correctUpdatedStudent.Username = "mica";
                correctUpdatedStudent.Privilege = StudentPrivilege.JuniorAdmin;
                correctUpdatedStudent.Description = "Pesimist";
                Assert.IsNotNull(correctUpdatedStudent, "Student to update not found");

                ResultMessage<StudentDTO> status = await executor.Update(correctUpdatedStudent, x => x.Username == "light");
                // Check status of operation
                Assert.AreEqual(status.Status, OperationStatus.Success);
                List<StudentDTO> students = await executor.GetAll<int>(dtoCondition: x => true);
                ResultMessage<StudentDTO> updatedStudent = await executor.GetSingleOrDefault((StudentDTO x) => x.Username == "mica");
                Assert.IsNotNull(updatedStudent.Result);

                // Check if inserted record exists with valid data
                Assert.AreEqual(updatedStudent.Result.Rating, correctUpdatedStudent.Rating, "Wrong rating");
                Assert.AreEqual(updatedStudent.Result.Name, correctUpdatedStudent.Name, "Wrong name");
                Assert.AreEqual(updatedStudent.Result.Username, correctUpdatedStudent.Username, "Wrong username");
                Assert.AreEqual(updatedStudent.Result.Privilege, correctUpdatedStudent.Privilege, "Wrong privilege");
                Assert.AreEqual(updatedStudent.Result.Description, correctUpdatedStudent.Description, "Wrong description");

                // Make sure ErrorHandler.Handle method was not called
                errHandlerMock.Verify(err => err.Log(It.IsAny<Exception>()), Times.Never);
            }
        }

        [TestMethod]
        public async Task GetAll()
        {
            int numberOfElements = 3;
            using (OrhedgeContext context = Utilities.CreateNewContext())
            {
                var errHandlerMock = new Mock<IErrorHandler>();
                var executor = new ServiceExecutor<StudentDTO, Student>(context, errHandlerMock.Object);

                List<StudentDTO> list = await executor.GetAll<int>(dtoCondition: x => x.Deleted == false);
                Assert.AreEqual(list.Count, numberOfElements);

                string existingUsername = "light";
                StudentDTO student = await executor.GetSingleOrDefault((StudentDTO x) => x.Username == existingUsername);
                Assert.IsTrue(list.Contains(student));

                // Make sure ErrorHandler.Handle method was not called
                errHandlerMock.Verify(err => err.Log(It.IsAny<Exception>()), Times.Never);
            }

        }

        [TestMethod]
        public async Task Count()
        {
            using (OrhedgeContext context = Utilities.CreateNewContext())
            {
                var errHandlerMock = new Mock<IErrorHandler>();

                IServicesExecutor<StudentDTO, Student> executor
                    = new ServiceExecutor<StudentDTO, Student>(context, errHandlerMock.Object);

                Assert.AreEqual(3, await executor.Count());
                Assert.AreEqual(1, await executor.Count(s => s.Username == "light"));
                errHandlerMock.Verify(err => err.Log(It.IsAny<Exception>()), Times.Never);
            }
        }
    }
}
