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
    public class TopicService : BaseService<TopicDTO, Topic>, ITopicService
    {
        public TopicService(IServicesExecutor<TopicDTO, Topic> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(TopicDTO topic)
            => await _servicesExecutor.Add(topic, x => false);


        public async Task<Status> Delete(int id)
        {
            Topic topic = await _servicesExecutor.GetOne(x => x.TopicId == id && !x.Deleted);
            topic.Deleted = true;
            return await _servicesExecutor.Delete(topic);
        }

        public async Task<TopicDTO> GetSingleOrDefault(Predicate<TopicDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);


        public Task<Status> Update(TopicDTO topic)
            => _servicesExecutor.Update(topic, x => x.TopicId == topic.TopicId && !x.Deleted);
    }
}
