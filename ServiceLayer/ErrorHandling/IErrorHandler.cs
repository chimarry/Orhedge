using ServiceLayer.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ErrorHandling
{
    public interface IErrorHandler
    {
        Task<DbStatus> HandleException(Exception exception);
    }
}
