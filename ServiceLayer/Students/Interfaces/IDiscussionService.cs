using ServiceLayer.DTO;

namespace ServiceLayer.Students.Interfaces
{
    public interface IDiscussionService : ICRUDServiceTemplate<DiscussionDTO>, ISelectableServiceTemplate<DiscussionDTO>
    {
    }
}
