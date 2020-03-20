using ServiceLayer.DTO;

namespace ServiceLayer.Students.Interfaces
{
    public interface ITopicService : ICRUDServiceTemplate<TopicDTO>, ISelectableServiceTemplate<TopicDTO>
    {
    }
}
