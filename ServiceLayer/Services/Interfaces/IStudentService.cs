using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IStudentService : ICRUDServiceTemplate<StudentDTO>, ISelectableServiceTemplate<StudentDTO>
    {
        Task<ResultMessage<bool>> ChangeRating(int studentId, double rating);
    }
}
