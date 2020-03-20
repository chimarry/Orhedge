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

        public async Task<Status> Add(DiscussionDTO topic)
            => await _servicesExecutor.Add(topic, x => false);


        public async Task<Status> Delete(int id)
        {
            Discussion discussion = await _servicesExecutor.GetOne(x => x.TopicId == id && !x.Deleted);
            discussion.Deleted = true;
            return await _servicesExecutor.Delete(discussion);
        }

        public async Task<DiscussionDTO> GetSingleOrDefault(Predicate<DiscussionDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);


        public Task<Status> Update(DiscussionDTO discussion)
            => _servicesExecutor.Update(discussion, x => x.TopicId == discussion.TopicId && !x.Deleted);
    }
}
