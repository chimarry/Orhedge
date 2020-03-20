using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Services
{
    public interface ITopicRatingService : ICRUDServiceTemplate<TopicRatingDTO>, ISelectableServiceTemplate<TopicRatingDTO>
    {
    }
}
