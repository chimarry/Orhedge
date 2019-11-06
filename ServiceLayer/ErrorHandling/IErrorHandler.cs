using System;

namespace ServiceLayer.ErrorHandling
{
    public interface IErrorHandler
    {
        void Handle(Exception exception);
    }
}
