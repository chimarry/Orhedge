using System;

namespace ServiceLayer.ErrorHandling.Exceptions
{
    public class ForumException : Exception
    {
        public ForumException()
        { }

        public ForumException(string message) : base(message)
        { }
    }
}
