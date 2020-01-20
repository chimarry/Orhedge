using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Students.Exceptions
{
    public class ForumException : Exception
    {
        public ForumException()
        { }

        public ForumException(string message) : base(message)
        { }
    }
}
