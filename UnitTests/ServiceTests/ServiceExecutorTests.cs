using DatabaseLayer;
using DatabaseLayer.Entity;
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

            using (OrhedgeContext context = DbUtilities.CreateNewContext())
            {
                int correctId = 3;
                var errHandlerMock = new Mock<IErrorHandler>();
                var executor = new ServiceExecutor<StudentDTO, Student>(context, errHandlerMock.Object);
                StudentDTO student = await executor.GetSingleOrDefault(x => x.StudentId == correctId);
                Assert.AreNotEqual(student, null);

                int wrongId = 0;
                StudentDTO noStudent = await executor.GetSingleOrDefault(x => x.StudentId == wrongId);
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

            using (OrhedgeContext context = DbUtilities.CreateNewContext())
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
            StudentDTO correctUpdatedStudent = new StudentDTO()
            {
                StudentId = 1,
                Index = "1161/16",
                Name = "Marija",
                Privilege = 0,
                Username = "light",
                Description = "Volim programiranje.",
                Rating = 4
            };
            using (OrhedgeContext context = DbUtilities.CreateNewContext())
            {
                var errHandlerMock = new Mock<IErrorHandler>();
                var executor = new ServiceExecutor<StudentDTO, Student>(context, errHandlerMock.Object);
                Status status = await executor.Update(correctUpdatedStudent, x => x.StudentId == correctUpdatedStudent.StudentId);
                // Check status of operation
                Assert.AreEqual(status, Status.SUCCESS);

                StudentDTO updatedStudent = await executor.GetSingleOrDefault(x => x.StudentId == correctUpdatedStudent.StudentId);
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
            using (OrhedgeContext context = DbUtilities.CreateNewContext())
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
    }
}
