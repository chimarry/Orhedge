using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.ErrorHandling
{
    public class CouldNotRegisterRootUserException : Exception
    {
        public CouldNotRegisterRootUserException(string message)
        : base(message) { }
    }
}
