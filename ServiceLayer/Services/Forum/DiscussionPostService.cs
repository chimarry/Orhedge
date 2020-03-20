using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class DiscussionPostService : BaseService<DiscussionPostDTO, DiscussionPost>, IDiscussionPostService
    {
        public DiscussionPostService(IServicesExecutor<DiscussionPostDTO, DiscussionPost> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(DiscussionPostDTO discussionPostDTO)
           => await _servicesExecutor.Add(discussionPostDTO, x => false);

        public async Task<Status> Delete(int id)
        {
            DiscussionPost dbDiscussionPost = await _servicesExecutor.GetOne(x => x.DiscussionPostId == id && x.Deleted == false);
            if (dbDiscussionPost == null)
            {
                return Status.NOT_FOUND;
            }
            dbDiscussionPost.Deleted = true;
            return await _servicesExecutor.Delete(dbDiscussionPost);
        }

        public async Task<DiscussionPostDTO> GetSingleOrDefault(Predicate<DiscussionPostDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<Status> Update(DiscussionPostDTO discussionPostDTO)
            => await _servicesExecutor.Update(discussionPostDTO, x => x.DiscussionPostId == discussionPostDTO.DiscussionPostId && x.Deleted == false);
    }
}
