using ServiceLayer.DTO;
using ServiceLayer.DTO.Student;
using ServiceLayer.ErrorHandling;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IStudentService : ICRUDServiceTemplate<StudentDTO>, ISelectableServiceTemplate<StudentDTO>
    {
        /// <summary>
        /// Updates rating for specified student.
        /// </summary>
        /// <param name="studentId">Unique identifier of the student</param>
        /// <param name="rating">New rating</param>
        /// <returns>True if updated, false if not</returns>
        Task<ResultMessage<bool>> UpdateRating(int studentId, double rating);

        /// <summary>
        /// Returns student with specified unique identifier.
        /// </summary>
        /// <param name="studentId">Unique identifier for the student</param>
        /// <returns>Null if student does not exist. If student deactivated profile, returns object of <see cref="DeletedStudentDTO"/></returns>
        Task<ResultMessage<StudentDTO>> GetStudentById(int studentId);
    }
}
