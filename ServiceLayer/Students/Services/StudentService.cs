using DatabaseLayer.Entity;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{

    public class StudentService : IStudentService
    {

        private readonly IServicesExecutor<StudentDTO, Student> _servicesExecutor;

        public StudentService(IServicesExecutor<StudentDTO, Student> servicesExecutor)
        {
            _servicesExecutor = servicesExecutor;
        }

        public async Task<Status> Add(StudentDTO studentDTO)
        {
            return await _servicesExecutor.Add(studentDTO, x => x.Username == studentDTO.Username && x.Deleted == false);
        }

        public async Task<Status> Delete(int id)
        {
            Student dbStudent = await _servicesExecutor.GetOne(x => x.StudentId == id && x.Deleted == false);
            dbStudent.Deleted = true;
            return await _servicesExecutor.Delete(dbStudent);
        }

        public async Task<Status> Update(StudentDTO studentDTO)
        {
            return await _servicesExecutor.Update(studentDTO, x => x.StudentId == studentDTO.StudentId && x.Deleted == false);
        }

        public async Task<List<StudentDTO>> GetAll()
        {
            return await _servicesExecutor.GetAll(x => x.Deleted == false);
        }

        public async Task<StudentDTO> GetById(int id)
        {
            return await _servicesExecutor.GetSingleOrDefault(x => x.StudentId == id && x.Deleted == false);
        }

        public async Task<List<StudentDTO>> GetRange(int startPosition, int numberOfItems)
        {
            return await _servicesExecutor.GetRange(startPosition, numberOfItems, x => x.Deleted == false);
        }

        public async Task<StudentDTO> GetSingleOrDefault(Predicate<StudentDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<List<StudentDTO>> GetAll<TKey>(Func<StudentDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetAll<TKey>(x => x.Deleted == false, sortKeySelector, asc);
        }

        public async Task<List<StudentDTO>> GetRange<TKey>(int offset, int num, Func<StudentDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetRange<TKey>(offset, num, x => x.Deleted == false, sortKeySelector, asc);
        }
    }
}

