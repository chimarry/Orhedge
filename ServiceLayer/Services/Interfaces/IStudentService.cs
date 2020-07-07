using ServiceLayer.DTO;
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
    }
}
