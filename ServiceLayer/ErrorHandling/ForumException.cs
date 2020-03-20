using System;
using System.Collections.Generic;
using System.Text;

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
