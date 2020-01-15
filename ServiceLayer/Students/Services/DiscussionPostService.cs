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
    public class DiscussionPostService : BaseService<DiscussionPostDTO, DiscussionPost>, IDiscussionPostService
    {
        public DiscussionPostService(IServicesExecutor<DiscussionPostDTO, DiscussionPost> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(DiscussionPostDTO discussionPostDTO)
        {
            return await _servicesExecutor.Add(discussionPostDTO, x => false);
        }

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

        public async Task<List<DiscussionPostDTO>> GetAll()
        {
            return await _servicesExecutor.GetAll(x => x.Deleted == false);
        }

        public async Task<List<DiscussionPostDTO>> GetAll<TKey>(Func<DiscussionPostDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetAll(x => x.Deleted == false, sortKeySelector, asc);
        }

        public async Task<DiscussionPostDTO> GetById(int id)
        {
            return await _servicesExecutor.GetSingleOrDefault(x => x.DiscussionPostId == id && x.Deleted == false);
        }

        public Task<List<DiscussionPostDTO>> GetRange(int startPosition, int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DiscussionPostDTO>> GetRange<TKey>(int offset, int num, Func<DiscussionPostDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetRange(offset, num, x => x.Deleted == false, sortKeySelector, asc);
        }

        public Task<List<DiscussionPostDTO>> GetRange<TKey>(int offset, int num, Predicate<DiscussionPostDTO> filter, Func<DiscussionPostDTO, TKey> sortKeySelector, bool asc = true)
        {
            throw new NotImplementedException();
        }

        public async Task<DiscussionPostDTO> GetSingleOrDefault(Predicate<DiscussionPostDTO> condition)
        {
            return await _servicesExecutor.GetSingleOrDefault(condition);
        }

        public async Task<Status> Update(DiscussionPostDTO discussionPostDTO)
        {
            return await _servicesExecutor.Update(discussionPostDTO, x => x.DiscussionPostId == discussionPostDTO.DiscussionPostId && x.Deleted == false);
        }
    }
}
