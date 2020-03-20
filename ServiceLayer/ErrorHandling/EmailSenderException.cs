using System;

namespace ServiceLayer.ErrorHandling.Exceptions
{
    public class EmailSenderException : Exception
    {
        public EmailSenderException(string message, Exception innerException)
            : base(message, innerException) { }

        public EmailSenderException(string message)
            : this(message, null) { }
    }
}
