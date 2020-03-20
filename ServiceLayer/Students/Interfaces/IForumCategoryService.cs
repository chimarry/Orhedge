using ServiceLayer.DTO;

namespace ServiceLayer.Students.Interfaces
{
    public interface IForumCategoryService : ICRUDServiceTemplate<ForumCategoryDTO>, ISelectableServiceTemplate<ForumCategoryDTO>
    {
    }
}
