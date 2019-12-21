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
    public class TopicService : ITopicService
    {

        private readonly IServicesExecutor<TopicDTO, Topic> _servicesExecutor;

        public TopicService(IServicesExecutor<TopicDTO, Topic> servicesExecutor)
            => _servicesExecutor = servicesExecutor;

        public async Task<Status> Add(TopicDTO topic)
            => await _servicesExecutor.Add(topic, x => false);
        

        public async Task<Status> Delete(int id)
        {
            Topic topic = await _servicesExecutor.GetOne(x => x.TopicId == id && !x.Deleted);
            topic.Deleted = true;
            return await _servicesExecutor.Delete(topic);
        }

        public async Task<List<TopicDTO>> GetAll()
            => await _servicesExecutor.GetAll(x => !x.Deleted);
        

        public async Task<List<TopicDTO>> GetAll<TKey>(Func<TopicDTO, TKey> sortKeySelector, bool asc = true)
            => await _servicesExecutor.GetAll(x => !x.Deleted, sortKeySelector, asc);

      
        public async Task<TopicDTO> GetById(int id)
            => await _servicesExecutor.GetSingleOrDefault(x => x.TopicId == id && !x.Deleted);


        public async Task<List<TopicDTO>> GetRange(int startPosition, int numberOfItems)
            => throw new NotImplementedException();
        

        public async Task<List<TopicDTO>> GetRange<TKey>(int offset, int num, Func<TopicDTO, TKey> sortKeySelector, bool asc = true)
            => await _servicesExecutor.GetRange(offset, num, x => !x.Deleted, sortKeySelector, asc);
        

        public async Task<TopicDTO> GetSingleOrDefault(Predicate<TopicDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);


        public Task<Status> Update(TopicDTO topic)
            => _servicesExecutor.Update(topic, x => x.TopicId == topic.TopicId && !x.Deleted);
    }
}
