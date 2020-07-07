using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using System;
using System.Threading.Tasks;
using StudentEntity = DatabaseLayer.Entity.Student;
namespace ServiceLayer.Services
{

    public class StudentService : BaseService<StudentDTO, StudentEntity>, IStudentService
    {
        public StudentService(IServicesExecutor<StudentDTO, StudentEntity> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<ResultMessage<StudentDTO>> Add(StudentDTO studentDTO)
             => await _servicesExecutor.Add(studentDTO, x => x.Username == studentDTO.Username && x.Deleted == false);

        public async Task<ResultMessage<bool>> Delete(int id)
            => await _servicesExecutor.Delete((StudentEntity x) => x.StudentId == id && !x.Deleted, x => { x.Deleted = true; return x; });

        public async Task<ResultMessage<StudentDTO>> Update(StudentDTO studentDTO)
            => await _servicesExecutor.Update(studentDTO, x => x.StudentId == studentDTO.StudentId && x.Deleted == false);

        public async Task<ResultMessage<StudentDTO>> GetSingleOrDefault(Predicate<StudentDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<ResultMessage<bool>> UpdateRating(int studentId, double rating)
        {
            StudentEntity student = await _servicesExecutor.GetSingleOrDefault((StudentEntity x) => x.StudentId == studentId && !x.Deleted);
            if (student == null)
                return new ResultMessage<bool>(false, OperationStatus.NotFound);
            student.Rating = rating;
            return await _servicesExecutor.SaveChanges();
        }
    }
}
