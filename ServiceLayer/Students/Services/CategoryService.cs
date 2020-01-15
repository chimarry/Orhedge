using DatabaseLayer;
using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class CategoryService : BaseService<CategoryDTO, Category>, ICategoryService
    {
        public CategoryService(IServicesExecutor<CategoryDTO, Category> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(CategoryDTO categoryDTO)
        {

            return await _servicesExecutor.Add(categoryDTO, x => x.Name == categoryDTO.Name && x.Deleted == false);

        }

        public async Task<Status> Delete(int id)
        {
            Category dbCategory = await _servicesExecutor.GetOne((x => x.CategoryId == id && x.Deleted == false));
            dbCategory.Deleted = true;
            return await _servicesExecutor.Delete(dbCategory);

        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            return await _servicesExecutor.GetAll(x => x.Deleted == false);

        }


        public async Task<CategoryDTO> GetById(int id)
        {
            return await _servicesExecutor.GetSingleOrDefault(x => x.CategoryId == id && x.Deleted == false);
        }

        public async Task<CategoryDTO> GetSingleOrDefault(Predicate<CategoryDTO> condition)
        {
            return await _servicesExecutor.GetSingleOrDefault(condition);
        }

        public async Task<List<CategoryDTO>> GetRange(int startPosition, int numberOfItems)
        {
            return await _servicesExecutor.GetRange(startPosition, numberOfItems, x => x.Deleted == false);
        }


        public async Task<Status> Update(CategoryDTO categoryDTO)
        {
            return await _servicesExecutor.Update(categoryDTO, x => x.CategoryId == categoryDTO.CategoryId && x.Deleted == false);

        }

        public async Task<List<CategoryDTO>> GetAll<TKey>(Func<CategoryDTO, TKey> sortKeySelector, bool asc)
        {
            return await _servicesExecutor.GetAll(x => x.Deleted == false, sortKeySelector, asc);
        }

        public async Task<List<CategoryDTO>> GetRange<TKey>(int offset, int num, Func<CategoryDTO, TKey> sortKeySelector, bool asc)
        {
            return await _servicesExecutor.GetRange(offset, num, x => x.Deleted == false, sortKeySelector, asc);
        }

        public Task<List<CategoryDTO>> GetRange<TKey>(int offset, int num, Predicate<CategoryDTO> filter, Func<CategoryDTO, TKey> sortKeySelector, bool asc = true)
        {
            throw new NotImplementedException();
        }
    }
}
