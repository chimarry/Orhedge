using DatabaseLayer;
using DatabaseLayer.Entity;
using DatabaseLayer.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
                List<StudentDTO> students = await executor.GetAll(s => true);
                StudentDTO student = await executor.GetSingleOrDefault(x => x.Username == "light");
                Assert.AreNotEqual(student, null);

                StudentDTO noStudent = await executor.GetSingleOrDefault(x => x.Username == "nonexistent");
                Assert.AreEqual(noStudent, null);

                // Make sure ErrorHandler.Handle method was not called
                errHandlerMock.Verify(err => err.Handle(It.IsAny<Exception>()), Times.Never);
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
                Status status = await executor.Add(correctStudent, s => s.Email == correctStudent.Email
                                               && s.Index == correctStudent.Index && s.Username == correctStudent.Username);

                // Check status of operation
                Assert.AreEqual(status, Status.SUCCESS);

                // Check if inserted record exists with valid data
                StudentDTO student = await executor.GetSingleOrDefault(x => x.Email == correctStudent.Email);
                Assert.AreNotEqual(student, null, "Wasn't added");
                Assert.AreEqual(student.Index, correctStudent.Index, "Wrong index");
                Assert.AreEqual(student.Username, correctStudent.Username, "Wrong username");
                Assert.AreEqual(student.Privilege, correctStudent.Privilege, "Wrong privlege");
                Assert.AreEqual(student.Name, correctStudent.Name, "Wrong name");
                Assert.AreEqual(student.LastName, correctStudent.LastName, "Wrong last name");

                // Make sure ErrorHandler.Handle method was not called
                errHandlerMock.Verify(err => err.Handle(It.IsAny<Exception>()), Times.Never);
            }
        }

        [TestMethod]
        public async Task Update()
        {

            using (OrhedgeContext context = Utilities.CreateNewContext())
            {
                var errHandlerMock = new Mock<IErrorHandler>();
                var executor = new ServiceExecutor<StudentDTO, Student>(context, errHandlerMock.Object);
                StudentDTO correctUpdatedStudent = await executor.GetSingleOrDefault(x => x.Username == "light");
                correctUpdatedStudent.Name = "Milica";
                correctUpdatedStudent.Rating = 3;
                correctUpdatedStudent.Username = "mica";
                correctUpdatedStudent.Privilege = StudentPrivilege.JuniorAdmin;
                correctUpdatedStudent.Description = "Pesimist";
                Assert.IsNotNull(correctUpdatedStudent, "Student to update not found");

                Status status = await executor.Update(correctUpdatedStudent, x => x.Username == "light");
                // Check status of operation
                Assert.AreEqual(status, Status.SUCCESS);
                List<StudentDTO> students = await executor.GetAll(s => true);
                StudentDTO updatedStudent = await executor.GetSingleOrDefault(x => x.Username == "mica");
                Assert.AreNotEqual(updatedStudent, null, "Not found");

                // Check if inserted record exists with valid data
                Assert.AreEqual(updatedStudent.Rating, correctUpdatedStudent.Rating, "Wrong rating");
                Assert.AreEqual(updatedStudent.Name, correctUpdatedStudent.Name, "Wrong name");
                Assert.AreEqual(updatedStudent.Username, correctUpdatedStudent.Username, "Wrong username");
                Assert.AreEqual(updatedStudent.Privilege, correctUpdatedStudent.Privilege, "Wrong privilege");
                Assert.AreEqual(updatedStudent.Description, correctUpdatedStudent.Description, "Wrong description");

                // Make sure ErrorHandler.Handle method was not called
                errHandlerMock.Verify(err => err.Handle(It.IsAny<Exception>()), Times.Never);
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

                List<StudentDTO> list = await executor.GetAll(x => x.Deleted == false);
                Assert.AreEqual(list.Count, numberOfElements);

                string existingUsername = "light";
                StudentDTO student = await executor.GetSingleOrDefault(x => x.Username == existingUsername);
                Assert.IsTrue(list.Contains(student));

                // Make sure ErrorHandler.Handle method was not called
                errHandlerMock.Verify(err => err.Handle(It.IsAny<Exception>()), Times.Never);
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
                errHandlerMock.Verify(err => err.Handle(It.IsAny<Exception>()), Times.Never);
            }
        }
    }
}
