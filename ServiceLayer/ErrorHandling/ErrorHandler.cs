using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;

namespace ServiceLayer.ErrorHandling
{
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
