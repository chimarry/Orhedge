using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{

    public class StudentService : BaseService<StudentDTO, DatabaseLayer.Entity.Student>, IStudentService
    {
        public StudentService(IServicesExecutor<StudentDTO, DatabaseLayer.Entity.Student> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<ResultMessage<StudentDTO>> Add(StudentDTO studentDTO)
             => await _servicesExecutor.Add(studentDTO, x => x.Username == studentDTO.Username && x.Deleted == false);

        public async Task<ResultMessage<bool>> Delete(int id)
            => await _servicesExecutor.Delete((DatabaseLayer.Entity.Student x) => x.StudentId == id && !x.Deleted, x => { x.Deleted = true; return x; });

        public async Task<ResultMessage<StudentDTO>> Update(StudentDTO studentDTO)
            => await _servicesExecutor.Update(studentDTO, x => x.StudentId == studentDTO.StudentId && x.Deleted == false);

        public async Task<ResultMessage<StudentDTO>> GetSingleOrDefault(Predicate<StudentDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);
    }
}
