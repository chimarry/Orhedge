using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public interface ICategoryService : ICRUDServiceTemplate<CategoryDTO>, ISelectableServiceTemplate<CategoryDTO>
    {

    }
}
