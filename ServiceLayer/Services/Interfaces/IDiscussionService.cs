using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public interface IDiscussionService : ICRUDServiceTemplate<DiscussionDTO>, ISelectableServiceTemplate<DiscussionDTO>
    {
    }
}
