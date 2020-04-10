using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class CategoryService : BaseService<CategoryDTO, Category>, ICategoryService
    {
        public CategoryService(IServicesExecutor<CategoryDTO, Category> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<ResultMessage<CategoryDTO>> Add(CategoryDTO categoryDTO)
            => await _servicesExecutor.Add(categoryDTO, x => x.Name == categoryDTO.Name && x.Deleted == false);

        public async Task<ResultMessage<bool>> Delete(int id)
            => await _servicesExecutor.Delete((Category x) => x.CategoryId == id && !x.Deleted, x => { x.Deleted = true; return x; });


        public async Task<ResultMessage<CategoryDTO>> GetSingleOrDefault(Predicate<CategoryDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);


        public async Task<ResultMessage<CategoryDTO>> Update(CategoryDTO categoryDTO)
            => await _servicesExecutor.Update(categoryDTO, x => x.CategoryId == categoryDTO.CategoryId && x.Deleted == false);
    }
}
