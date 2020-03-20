using ServiceLayer.DTO;

namespace ServiceLayer.Students.Interfaces
{
    public interface IDiscussionPostService : ICRUDServiceTemplate<DiscussionPostDTO>, ISelectableServiceTemplate<DiscussionPostDTO>
    {
    }
}
