using ServiceLayer.DTO;

namespace ServiceLayer.Students.Interfaces
{
    public interface IStudentService : ICRUDServiceTemplate<StudentDTO>, ISelectableServiceTemplate<StudentDTO>
    {
    }
}
