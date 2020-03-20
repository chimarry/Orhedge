using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class CategoryService : BaseService<CategoryDTO, Category>, ICategoryService
    {
        public CategoryService(IServicesExecutor<CategoryDTO, Category> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(CategoryDTO categoryDTO)
            => await _servicesExecutor.Add(categoryDTO, x => x.Name == categoryDTO.Name && x.Deleted == false);

        public async Task<Status> Delete(int id)
        {
            Category dbCategory = await _servicesExecutor.GetOne((x => x.CategoryId == id && x.Deleted == false));
            dbCategory.Deleted = true;
            return await _servicesExecutor.Delete(dbCategory);

        }

        public async Task<CategoryDTO> GetSingleOrDefault(Predicate<CategoryDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);


        public async Task<Status> Update(CategoryDTO categoryDTO)
            => await _servicesExecutor.Update(categoryDTO, x => x.CategoryId == categoryDTO.CategoryId && x.Deleted == false);
    }
}
