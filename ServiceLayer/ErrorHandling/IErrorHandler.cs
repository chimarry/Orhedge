using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace ServiceLayer.ErrorHandling
{
    public interface IErrorHandler
    {
        void Log(Exception exception);

        OperationStatus Handle(DbUpdateException ex);

        OperationStatus Handle(AutoMapperMappingException ex);

        OperationStatus Handle(Exception ex);
    }
}
