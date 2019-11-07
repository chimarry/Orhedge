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
    public class CategoryService : ICategoryService
    {
        private readonly IServicesExecutor<CategoryDTO, Category> _servicesExecutor;
        public CategoryService(IServicesExecutor<CategoryDTO, Category> servicesExecutor)
        {
            _servicesExecutor = servicesExecutor;
        }

        public async Task<Status> Add(CategoryDTO categoryDTO)
        {

            return await _servicesExecutor.Add(categoryDTO, x => x.Name == categoryDTO.Name && x.Deleted == false);

        }

        public async Task<Status> Delete(int id)
        {
            Category dbCategory = await _servicesExecutor.GetSingleOrDefault((x => x.CategoryId == id && x.Deleted == false));
            dbCategory.Deleted = true;
            return await _servicesExecutor.Delete(dbCategory);

        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            return await _servicesExecutor.GetAll(x => x.Deleted == false);

        }


        public async Task<CategoryDTO> GetById(int id)
        {
            return await _servicesExecutor.GetOne(x => x.CategoryId == id && x.Deleted == false);
        }

        public Task<CategoryDTO> GetOne(Predicate<CategoryDTO> condition)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryDTO>> GetRange(int startPosition, int numberOfItems)
        {
            return await _servicesExecutor.GetRange(startPosition, numberOfItems, x => x.Deleted == false);
        }

        public async Task<Status> Update(CategoryDTO categoryDTO)
        {
            return await _servicesExecutor.Update(categoryDTO, x => x.CategoryId == categoryDTO.CategoryId && x.Deleted == false);

        }

    }
}
