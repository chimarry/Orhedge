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
             => await _servicesExecutor.Add(courseDTO, x => x.Name != courseDTO.Name && x.Deleted == false);

        public async Task<Status> Delete(int id)
        {
            Course dbCourse = await _servicesExecutor.GetOne((x => x.CourseId == id && x.Deleted == false));
            dbCourse.Deleted = true;
            return await _servicesExecutor.Delete(dbCourse);
        }
        public async Task<CourseDTO> GetSingleOrDefault(Predicate<CourseDTO> condition)
             => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<Status> Update(CourseDTO courseDTO)
            => await _servicesExecutor.Update(courseDTO, x => x.CourseId == courseDTO.CourseId && x.Deleted == false);
    }
}

