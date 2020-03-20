using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public interface ITopicService : ICRUDServiceTemplate<TopicDTO>, ISelectableServiceTemplate<TopicDTO>
    {
    }
}
