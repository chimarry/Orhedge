using ServiceLayer.DTO;

namespace ServiceLayer.Students.Interfaces
{
    public interface ICategoryService : ICRUDServiceTemplate<CategoryDTO>, ISelectableServiceTemplate<CategoryDTO>
    {

    }
}
