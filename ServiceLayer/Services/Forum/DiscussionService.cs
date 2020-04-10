using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class DiscussionService : BaseService<DiscussionDTO, Discussion>, IDiscussionService
    {
        public DiscussionService(IServicesExecutor<DiscussionDTO, Discussion> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<ResultMessage<DiscussionDTO>> Add(DiscussionDTO topic)
            => await _servicesExecutor.Add(topic, x => false);


        public async Task<ResultMessage<bool>> Delete(int id)
             => await _servicesExecutor.Delete((Discussion x) => x.TopicId == id && !x.Deleted, x => { x.Deleted = true; return x; });

        public async Task<ResultMessage<DiscussionDTO>> GetSingleOrDefault(Predicate<DiscussionDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);


        public Task<ResultMessage<DiscussionDTO>> Update(DiscussionDTO discussion)
            => _servicesExecutor.Update(discussion, x => x.TopicId == discussion.TopicId && !x.Deleted);
    }
}
