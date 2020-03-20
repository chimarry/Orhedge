using DatabaseLayer.Entity;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{

    public class StudentService : BaseService<StudentDTO, Student>, IStudentService
    {
        public StudentService(IServicesExecutor<StudentDTO, Student> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(StudentDTO studentDTO)
             => await _servicesExecutor.Add(studentDTO, x => x.Username == studentDTO.Username && x.Deleted == false);

        public async Task<Status> Delete(int id)
        {
            Student dbStudent = await _servicesExecutor.GetOne(x => x.StudentId == id && x.Deleted == false);
            dbStudent.Deleted = true;
            return await _servicesExecutor.Delete(dbStudent);
        }

        public async Task<Status> Update(StudentDTO studentDTO)
            => await _servicesExecutor.Update(studentDTO, x => x.StudentId == studentDTO.StudentId && x.Deleted == false);

        public async Task<StudentDTO> GetSingleOrDefault(Predicate<StudentDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);
    }
}
