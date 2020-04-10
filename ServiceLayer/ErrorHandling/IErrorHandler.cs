using Microsoft.EntityFrameworkCore;
using System;

namespace ServiceLayer.ErrorHandling
{
    public interface IErrorHandler
    {
        void Handle(Exception exception);
        OperationStatus Handle(DbUpdateException ex);
    }
}
