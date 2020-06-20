using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ForumCategoryService : BaseService<ForumCategoryDTO, ForumCategory>, IForumCategoryService
    {
        public ForumCategoryService(IServicesExecutor<ForumCategoryDTO, ForumCategory> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<ResultMessage<ForumCategoryDTO>> Add(ForumCategoryDTO forumCategoryDTO)
          => await _servicesExecutor.Add(forumCategoryDTO, x => x.Name == forumCategoryDTO.Name);

        public Task<ResultMessage<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultMessage<ForumCategoryDTO>> GetSingleOrDefault(Predicate<ForumCategoryDTO> condition)
          => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<ResultMessage<ForumCategoryDTO>> Update(ForumCategoryDTO forumCategoryDTO)
          => await _servicesExecutor.Update(forumCategoryDTO, x => x.ForumCategoryId == forumCategoryDTO.ForumCategoryId);
    }
}
