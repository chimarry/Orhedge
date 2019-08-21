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
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class CourseService : ICourseService
    {
        private readonly OrhedgeContext _context;
        private readonly IErrorHandler _errorHandler;
        private readonly IServicesExecutor<CourseDTO, Course> _servicesExecutor;
        public CourseService(OrhedgeContext context, IErrorHandler errorHandler, IServicesExecutor<CourseDTO, Course> servicesExecutor)
        {
            _context = context;
            _errorHandler = errorHandler;
            _servicesExecutor = servicesExecutor;
        }

        public async Task<DbStatus> Add(CourseDTO courseDTO)
        {
            try
            {
                await _servicesExecutor.TryAdding(courseDTO, x => x.Name == courseDTO.Name && courseDTO.Semester == x.Semester && courseDTO.StudyYear == x.StudyYear && x.Deleted == false);
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
                var dbCourse = await _servicesExecutor.GetSingleOrDefault((x => x.CourseId == id && x.Deleted == false));
                dbCourse.Deleted = true;
                await _servicesExecutor.TryDeleting(dbCourse);
                return DbStatus.SUCCESS;
            }
            catch (Exception ex)
            {
                return await _errorHandler.HandleException(ex);
            }
        }

        public async Task<IList<CourseDTO>> GetAll()
        {
            try
            {
                return await _servicesExecutor.TryGettingAll(x => x.Deleted == false);
            }
            catch (Exception ex)
            {
                await _errorHandler.HandleException(ex);
                return new List<CourseDTO>();
            }
        }


        public async Task<CourseDTO> GetById(int id)
        {
            try
            {
                return await _servicesExecutor.TryGettingOne(x => x.CourseId == id && x.Deleted == false);
            }
            catch (Exception ex)
            {
                await _errorHandler.HandleException(ex);
                return null;
            }
        }

        public Task<IList<CourseDTO>> GetRange(int startPosition, int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public async Task<DbStatus> Update(CourseDTO courseDTO)
        {
            try
            {
                await _servicesExecutor.TryUpdating(courseDTO, x => x.CourseId == courseDTO.CourseId && x.Deleted == false);
                return DbStatus.SUCCESS;
            }
            catch (Exception ex)
            {
                return await _errorHandler.HandleException(ex);
            }
        }
    }
}

