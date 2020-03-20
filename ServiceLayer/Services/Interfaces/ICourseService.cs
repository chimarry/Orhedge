using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public interface ICourseService : ICRUDServiceTemplate<CourseDTO>, ISelectableServiceTemplate<CourseDTO>
    {
    }
}
