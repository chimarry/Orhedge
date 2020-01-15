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
    public class ForumCategoryService : BaseService<ForumCategoryDTO, ForumCategory>, IForumCategoryService
    {
        public ForumCategoryService(IServicesExecutor<ForumCategoryDTO, ForumCategory> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(ForumCategoryDTO forumCategoryDTO)
        {
            return await _servicesExecutor.Add(forumCategoryDTO, x => x.Name == forumCategoryDTO.Name);
        }

        public async Task<Status> Delete(int id)
        {
            ForumCategory dbForumCategory = await _servicesExecutor.GetOne(x => x.ForumCategoryId == id);
            if (dbForumCategory == null)
            {
                return Status.NOT_FOUND;
            }
            return await _servicesExecutor.Delete(dbForumCategory);  // not complete 
        }

        public async Task<List<ForumCategoryDTO>> GetAll()
        {
            return await _servicesExecutor.GetAll(x => true);
        }

        public async Task<List<ForumCategoryDTO>> GetAll<TKey>(Func<ForumCategoryDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetAll(x => true, sortKeySelector, asc);
        }

        public async Task<ForumCategoryDTO> GetById(int id)
        {
            return await _servicesExecutor.GetSingleOrDefault(x => x.ForumCategoryId == id);
        }

        public Task<List<ForumCategoryDTO>> GetRange(int startPosition, int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ForumCategoryDTO>> GetRange<TKey>(int offset, int num, Func<ForumCategoryDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetRange(offset, num, x => true, sortKeySelector, asc);
        }

        public Task<List<ForumCategoryDTO>> GetRange<TKey>(int offset, int num, Predicate<ForumCategoryDTO> filter, Func<ForumCategoryDTO, TKey> sortKeySelector, bool asc = true)
        {
            throw new NotImplementedException();
        }

        public async Task<ForumCategoryDTO> GetSingleOrDefault(Predicate<ForumCategoryDTO> condition)
        {
            return await _servicesExecutor.GetSingleOrDefault(condition);
        }

        public async Task<Status> Update(ForumCategoryDTO forumCategoryDTO)
        {
            return await _servicesExecutor.Update(forumCategoryDTO, x => x.ForumCategoryId == forumCategoryDTO.ForumCategoryId);
        }

        public async Task<int> Count()
            => await _servicesExecutor.Count();
    }
}
