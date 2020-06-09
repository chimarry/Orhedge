using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class TopicRatingService : BaseService<TopicRatingDTO, TopicRating>, ITopicRatingService
    {
        public TopicRatingService(IServicesExecutor<TopicRatingDTO, TopicRating> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<ResultMessage<TopicRatingDTO>> Add(TopicRatingDTO topicRatingDTO)
           => await _servicesExecutor.Add(topicRatingDTO, x => false);

        public Task<ResultMessage<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<ResultMessage<TopicRatingDTO>> GetSingleOrDefault(Predicate<TopicRatingDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<ResultMessage<TopicRatingDTO>> Update(TopicRatingDTO topicRatingDTO)
           => await _servicesExecutor.Update(topicRatingDTO, x => x.TopicId == topicRatingDTO.TopicId && x.StudentId == topicRatingDTO.StudentId);
    }
}
