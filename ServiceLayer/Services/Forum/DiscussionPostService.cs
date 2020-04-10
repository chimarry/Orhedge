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

        public async Task<ResultMessage<DiscussionPostDTO>> Add(DiscussionPostDTO discussionPostDTO)
           => await _servicesExecutor.Add(discussionPostDTO, x => false);

        public async Task<ResultMessage<bool>> Delete(int id)
          => await _servicesExecutor.Delete((DiscussionPost x) => x.DiscussionPostId == id && !x.Deleted, x => { x.Deleted = true; return x; });

        public async Task<ResultMessage<DiscussionPostDTO>> GetSingleOrDefault(Predicate<DiscussionPostDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<ResultMessage<DiscussionPostDTO>> Update(DiscussionPostDTO discussionPostDTO)
            => await _servicesExecutor.Update(discussionPostDTO, x => x.DiscussionPostId == discussionPostDTO.DiscussionPostId && x.Deleted == false);
    }
}
