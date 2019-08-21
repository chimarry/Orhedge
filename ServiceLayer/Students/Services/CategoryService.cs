using DatabaseLayer;
using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.AutoMapper;
using ServiceLayer.DTO;
using ServiceLayer.Enum;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly OrhedgeContext _context;
        private readonly IErrorHandler _errorHandler;
        private readonly IServicesExecutor<CategoryDTO, Category> _servicesExecutor;
        public CategoryService(OrhedgeContext context, IErrorHandler errorHandler, IServicesExecutor<CategoryDTO, Category> servicesExecutor)
        {
            _context = context;
            _errorHandler = errorHandler;
            _servicesExecutor = servicesExecutor;
        }

        public async Task<DbStatus> Add(CategoryDTO categoryDTO)
        {
            try
            {

                await _servicesExecutor.TryAdding(categoryDTO, x => x.Name == categoryDTO.Name && x.Deleted == false);
                return DbStatus.SUCCESS;
            }
            catch (Exception ex)
            {
                return await _errorHandler.HandleException(ex);
            }
        }

        public async Task<DbStatus> Delete(int id)
        {
            try
            {
                var dbCategory = await _servicesExecutor.GetSingleOrDefault((x => x.CategoryId == id && x.Deleted == false));
                dbCategory.Deleted = true;
                await _servicesExecutor.TryDeleting(dbCategory);
                return DbStatus.SUCCESS;
            }
            catch (Exception ex)
            {
                return await _errorHandler.HandleException(ex);
            }
        }

        public async Task<IList<CategoryDTO>> GetAll()
        {
            try
            {
                return await _servicesExecutor.TryGettingAll(x => x.Deleted == false);
            }
            catch (Exception ex)
            {
                await _errorHandler.HandleException(ex);
                return new List<CategoryDTO>();
            }
        }


        public async Task<CategoryDTO> GetById(int id)
        {
            try
            {
                return await _servicesExecutor.TryGettingOne(x => x.CategoryId == id && x.Deleted == false);
            }
            catch (Exception ex)
            {
                await _errorHandler.HandleException(ex);
                return null;
            }
        }

        public Task<IList<CategoryDTO>> GetRange(int startPosition, int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public async Task<DbStatus> Update(CategoryDTO categoryDTO)
        {
            try
            {
                await _servicesExecutor.TryUpdating(categoryDTO, x => x.CategoryId == categoryDTO.CategoryId && x.Deleted == false);
                return DbStatus.SUCCESS;
            }
            catch (Exception ex)
            {
                return await _errorHandler.HandleException(ex);
            }
        }

    }
}
