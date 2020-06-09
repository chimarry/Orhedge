using System;

namespace ServiceLayer.ErrorHandling
{
    public class CouldNotRegisterRootUserException : Exception
    {
        public CouldNotRegisterRootUserException(string message)
        : base(message) { }
    }
}
