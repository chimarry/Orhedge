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
    public class ForumCategoryService : BaseService<ForumCategoryDTO, ForumCategory>, IForumCategoryService
    {
        public ForumCategoryService(IServicesExecutor<ForumCategoryDTO, ForumCategory> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(ForumCategoryDTO forumCategoryDTO)
          => await _servicesExecutor.Add(forumCategoryDTO, x => x.Name == forumCategoryDTO.Name);

        public async Task<Status> Delete(int id)
        {
            ForumCategory dbForumCategory = await _servicesExecutor.GetOne(x => x.ForumCategoryId == id);
            if (dbForumCategory == null)
            {
                return Status.NOT_FOUND;
            }
            return await _servicesExecutor.Delete(dbForumCategory);  // Not complete 
        }

        public async Task<ForumCategoryDTO> GetSingleOrDefault(Predicate<ForumCategoryDTO> condition)
          => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<Status> Update(ForumCategoryDTO forumCategoryDTO)
          => await _servicesExecutor.Update(forumCategoryDTO, x => x.ForumCategoryId == forumCategoryDTO.ForumCategoryId);
    }
}
