using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public interface IForumCategoryService : ICRUDServiceTemplate<ForumCategoryDTO>, ISelectableServiceTemplate<ForumCategoryDTO>
    {
    }
}
