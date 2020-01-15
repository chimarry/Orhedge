using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class TopicRatingService : BaseService<TopicRatingDTO, TopicRating>, ITopicRatingService
    {
        public TopicRatingService(IServicesExecutor<TopicRatingDTO, TopicRating> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(TopicRatingDTO topicRatingDTO)
        {
            return await _servicesExecutor.Add(topicRatingDTO, x => false);
        }

        public Task<Status> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TopicRatingDTO>> GetAll()
        {
            return await _servicesExecutor.GetAll(x => true);
        }

        public async Task<List<TopicRatingDTO>> GetAll<TKey>(Func<TopicRatingDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetAll(x => true, sortKeySelector, asc);
        }

        public Task<TopicRatingDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TopicRatingDTO>> GetRange(int startPosition, int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TopicRatingDTO>> GetRange<TKey>(int offset, int num, Func<TopicRatingDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetRange(offset, num, x => true, sortKeySelector, asc);
        }

        public Task<List<TopicRatingDTO>> GetRange<TKey>(int offset, int num, Predicate<TopicRatingDTO> filter, Func<TopicRatingDTO, TKey> sortKeySelector, bool asc = true)
        {
            throw new NotImplementedException();
        }

        public async Task<TopicRatingDTO> GetSingleOrDefault(Predicate<TopicRatingDTO> condition)
        {
            return await _servicesExecutor.GetSingleOrDefault(condition);
        }

        public async Task<Status> Update(TopicRatingDTO topicRatingDTO)
        {
            return await _servicesExecutor.Update(topicRatingDTO, x => x.TopicId == topicRatingDTO.TopicId && x.StudentId == topicRatingDTO.StudentId);
        }
    }
}
