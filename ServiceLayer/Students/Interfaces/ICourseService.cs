using ServiceLayer.DTO;

namespace ServiceLayer.Students.Interfaces
{
    public interface ICourseService : ICRUDServiceTemplate<CourseDTO>, ISelectableServiceTemplate<CourseDTO>
    {
    }
}
