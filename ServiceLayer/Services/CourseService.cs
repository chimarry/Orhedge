using DatabaseLayer;
using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class CourseService : BaseService<CourseDTO, Course>, ICourseService
    {
        public CourseService(IServicesExecutor<CourseDTO, Course> servicesExecutor)
             : base(servicesExecutor) { }

        public async Task<ResultMessage<CourseDTO>> Add(CourseDTO courseDTO)
             => await _servicesExecutor.Add(courseDTO, x => x.Name != courseDTO.Name && x.Deleted == false);

        public async Task<ResultMessage<bool>> Delete(int id)
            => await _servicesExecutor.Delete((Course x) => x.CourseId == id && !x.Deleted, x => { x.Deleted = true; return x; });

        public async Task<ResultMessage<CourseDTO>> GetSingleOrDefault(Predicate<CourseDTO> condition)
             => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<ResultMessage<CourseDTO>> Update(CourseDTO courseDTO)
            => await _servicesExecutor.Update(courseDTO, x => x.CourseId == courseDTO.CourseId && x.Deleted == false);
    }
}

