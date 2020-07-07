using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;

namespace ServiceLayer.ErrorHandling
{
    /// <summary>
    /// This class should be used to, based on the type of the exception, logg certain information about the event 
    /// that occured, and return appropriate error code with detailed message.
    /// </summary>
    public class ErrorHandler : IErrorHandler
    {
        private readonly ILogger<ErrorHandler> _logger;

        public const int SqlServerViolationOfUniqueIndex = 2601;
        public const int SqlServerViolationOfUniqueConstraint = 2627;
        public const int SqlNetworkConnectionError = 17829;

        public ErrorHandler(ILogger<ErrorHandler> logger) => (_logger) = (logger);

        public void Handle(Exception exception)
        => _logger.Log(LogLevel.Error, exception.Message);

        public OperationStatus Handle(DbUpdateException ex)
        {
            Handle(ex);

            if (!(ex?.InnerException is SqlException sqlEx))
                return OperationStatus.UnknownError;
            switch (sqlEx.Number)
            {
                case SqlServerViolationOfUniqueIndex: return OperationStatus.InvalidData;
                case SqlServerViolationOfUniqueConstraint: return OperationStatus.InvalidData;
                case SqlNetworkConnectionError: return OperationStatus.DatabaseError;
                default: return OperationStatus.InvalidData;
            };
        }
    }
}
