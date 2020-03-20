using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public interface IDiscussionPostService : ICRUDServiceTemplate<DiscussionPostDTO>, ISelectableServiceTemplate<DiscussionPostDTO>
    {
    }
}
