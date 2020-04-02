using System;

namespace ServiceLayer.ErrorHandling.Exceptions
{
    public class DatabaseErrorException : Exception
    {
        public DatabaseErrorException(string message)
            : base(message) { }
        public DatabaseErrorException()
        {

        }
    }
}
