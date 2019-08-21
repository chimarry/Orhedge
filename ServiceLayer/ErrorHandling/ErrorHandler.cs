using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ServiceLayer.Enum;

namespace ServiceLayer.ErrorHandling
{
    public class ErrorHandler : IErrorHandler
    {
        private readonly ILogger<ErrorHandler> _logger;

        //constants just here as an example of potential errors, that is messages that should be placed in log file =>> NEED FIXING
        private static readonly string NO_SUCH_TABLE = "Used table could not be found.";
        private static readonly string OUT_OF_RESOURCES = "The server is out of resources, check if MySql or some other process is using all available memory.";
        private static readonly string NO_SORT_MEMORY = "The server is out of sort-memory, the sort buffer size should be increased.";
        private static readonly string UNKNOWN_COMMAND = "The command is unknown.";
        private static readonly string DUPLICATE_KEY = "There is already a key with the given values.";
        private static readonly string KEY_NOT_FOUND = "The specified key was not found.";
        private static readonly string TABLE_NOT_LOCKED_FOR_WRITE = "The specified table was locked with a READ lock, and can't be updated.";
        private static readonly string WRONG_TABLE_NAME = "he specified table name is incorrect.";
        private static readonly string BAD_FIELD_NAME = "The specified columns is unknown.";
        public ErrorHandler(ILogger<ErrorHandler> logger)
        {
            _logger = logger;
        }
        public Task<DbStatus> HandleException(Exception exception)
        {
            //TODO: Add logic to this error handler so that it logs details of thrown exception for the programmer in 
            //log file corresponding log level, and than return one of the DbStatus codes to the Application layer or other
            // project referencing this library, so that user gets appropriate message. For example, he needs to know that there has been
            // a server error for couple of messages EF Core will return in that case ( OUT_OF_RESOURCES,NO_SORT_MEMORY...)
            throw new NotImplementedException();
        }
    }
}
