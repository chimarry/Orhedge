using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class TopicService : BaseService<TopicDTO, Topic>, ITopicService
    {
        public TopicService(IServicesExecutor<TopicDTO, Topic> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<ResultMessage<TopicDTO>> Add(TopicDTO topic)
            => await _servicesExecutor.Add(topic, x => false);


        public async Task<ResultMessage<bool>> Delete(int id)
           => await _servicesExecutor.Delete((Topic x) => x.TopicId == id && !x.Deleted, x => { x.Deleted = true; return x; });

        public async Task<ResultMessage<TopicDTO>> GetSingleOrDefault(Predicate<TopicDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);


        public async Task<ResultMessage<TopicDTO>> Update(TopicDTO topic)
            => await _servicesExecutor.Update(topic, x => x.TopicId == topic.TopicId && !x.Deleted);
    }
}
