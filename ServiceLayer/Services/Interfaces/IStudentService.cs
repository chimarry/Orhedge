using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public interface IStudentService : ICRUDServiceTemplate<StudentDTO>, ISelectableServiceTemplate<StudentDTO>
    {
    }
}
