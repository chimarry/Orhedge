using DatabaseLayer;
using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.Enum;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{

    public class StudentService : IStudentService
    {
        private readonly IErrorHandler _errorHandler;
        private readonly OrhedgeContext _context;
        private readonly IServicesExecutor<StudentDTO, Student> _servicesExecutor;

        public StudentService(OrhedgeContext context, IErrorHandler errorHandler, IServicesExecutor<StudentDTO, Student> servicesExecutor)
        {
            _context = context;
            _errorHandler = errorHandler;
            _servicesExecutor = servicesExecutor;
        }
        public async Task<DbStatus> Add(StudentDTO studentDTO)
        {
            try
            {
                await _servicesExecutor.TryAdding(studentDTO, x => x.Username == studentDTO.Username && x.Deleted == false);
                return DbStatus.SUCCESS;
            }
            catch (Exception ex)
            {
                return await _errorHandler.HandleException(ex);
            }
        }

        public async Task<DbStatus> Delete(int id)
        {
            try
            {
                var dbStudent = await _servicesExecutor.GetSingleOrDefault((x => x.StudentId == id && x.Deleted == false));
                dbStudent.Deleted = true;
                await _servicesExecutor.TryDeleting(dbStudent);
                return DbStatus.SUCCESS;
            }
            catch (Exception ex)
            {
                return await _errorHandler.HandleException(ex);
            }
        }
        public async Task<DbStatus> Update(StudentDTO studentDTO)
        {
            try
            {
                await _servicesExecutor.TryUpdating(studentDTO, x => x.StudentId == studentDTO.StudentId && x.Deleted == false);
                return DbStatus.SUCCESS;
            }
            catch (Exception ex)
            {
                return await _errorHandler.HandleException(ex);
            }
        }

        public async Task<IList<StudentDTO>> GetAll()
        {
            try
            {
                return await _servicesExecutor.TryGettingAll(x => x.Deleted == false);
            }
            catch (Exception ex)
            {
                await _errorHandler.HandleException(ex);
                return new List<StudentDTO>();
            }
        }

        public async Task<StudentDTO> GetById(int id)
        {
            try
            {
                return await _servicesExecutor.TryGettingOne(x => x.StudentId == id && x.Deleted == false);
            }
            catch (Exception ex)
            {
                await _errorHandler.HandleException(ex);
                return null;
            }
        }

        public Task<IList<StudentDTO>> GetRange(int startPosition, int numberOfItems)
        {
            throw new NotImplementedException();
        }
    }
}
