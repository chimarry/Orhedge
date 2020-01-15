using DatabaseLayer;
using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class CourseService : BaseService<CourseDTO, Course>, ICourseService
    {
        public CourseService(IServicesExecutor<CourseDTO, Course> servicesExecutor)
             : base(servicesExecutor) { }

        public async Task<Status> Add(CourseDTO courseDTO)
        {
            await _servicesExecutor.Add(courseDTO, x => x.Name != courseDTO.Name && x.Deleted == false);
            return Status.SUCCESS;
        }

        public async Task<Status> Delete(int id)
        {
            Course dbCourse = await _servicesExecutor.GetOne((x => x.CourseId == id && x.Deleted == false));
            dbCourse.Deleted = true;
            return await _servicesExecutor.Delete(dbCourse);
        }

        public async Task<List<CourseDTO>> GetAll()
        {
            return await _servicesExecutor.GetAll(x => x.Deleted == false);
        }

        public async Task<List<CourseDTO>> GetAll<TKey>(Func<CourseDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetAll<TKey>(x => x.Deleted == false, sortKeySelector, asc);
        }

        public async Task<CourseDTO> GetById(int id)
        {
            return await _servicesExecutor.GetSingleOrDefault(x => x.CourseId == id && x.Deleted == false);
        }

        public async Task<CourseDTO> GetSingleOrDefault(Predicate<CourseDTO> condition)
        {
            return await _servicesExecutor.GetSingleOrDefault(condition);
        }

        public async Task<List<CourseDTO>> GetRange(int startPosition, int numberOfItems)
        {
            return await _servicesExecutor.GetRange(startPosition, numberOfItems, x => x.Deleted == false);
        }

        public async Task<List<CourseDTO>> GetRange<TKey>(int offset, int num, Func<CourseDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetRange<TKey>(offset, num, x => x.Deleted == false, sortKeySelector, asc);
        }

        public async Task<Status> Update(CourseDTO courseDTO)
        {
            return await _servicesExecutor.Update(courseDTO, x => x.CourseId == courseDTO.CourseId && x.Deleted == false);
        }

        public Task<List<CourseDTO>> GetRange<TKey>(int offset, int num, Predicate<CourseDTO> filter, Func<CourseDTO, TKey> sortKeySelector, bool asc = true)
        {
            throw new NotImplementedException();
        }
    }
}

