
using DatabaseLayer;
using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitTests.Common;

namespace UnitTests.ServiceTests
{
    [TestClass]
    public class ServiceExecutorTests
    {

        private readonly RegistrationDTO _regDTO = new RegistrationDTO
        {
            Email = "test@test.com",
            RegistrationCode = "code",
            RegistrationId = 1,
            Timestamp = DateTime.UtcNow,
            Used = false
        };

        [TestMethod]
        public async Task Add()
        {
            using (OrhedgeContext context = DbUtilities.CreateNewContext())
            {
                var errHandlerMock = new Mock<IErrorHandler>();
                var executor = new ServiceExecutor<RegistrationDTO, Registration>(context, errHandlerMock.Object);
                await executor.Add(_regDTO, regDTO => regDTO.RegistrationId == _regDTO.RegistrationId);

                // Check if inserted record exists
                bool exists = context.Registrations.Any(reg => reg.RegistrationId == _regDTO.RegistrationId);
                Assert.IsTrue(exists, "Registration does not exists in database");

                // Make sure ErrorHandler.Handle method was not called
                errHandlerMock.Verify(err => err.Handle(It.IsAny<Exception>()), Times.Never);
            }
        }
    }
}
