using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public interface ITopicRatingService : ICRUDServiceTemplate<TopicRatingDTO>, ISelectableServiceTemplate<TopicRatingDTO>
    {
    }
}
