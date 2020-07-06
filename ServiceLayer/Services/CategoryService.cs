using DatabaseLayer;
using DatabaseLayer.Entity;
using Microsoft.EntityFrameworkCore.Storage;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class CategoryService : BaseService<CategoryDTO, Category>, ICategoryService
    {
        private readonly OrhedgeContext _context;
        private readonly IStudyMaterialService _studyMaterialService;
        public CategoryService(IServicesExecutor<CategoryDTO, Category> servicesExecutor, IStudyMaterialService studyMaterialService, OrhedgeContext context)
            : base(servicesExecutor) => (_context, _studyMaterialService) = (context, studyMaterialService);

        public async Task<ResultMessage<CategoryDTO>> Add(CategoryDTO categoryDTO)
            => await _servicesExecutor.Add(categoryDTO, x => x.Name == categoryDTO.Name && x.CourseId == categoryDTO.CourseId && x.Deleted == false);

        public async Task<ResultMessage<bool>> Delete(int id)
        {
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {
                ResultMessage<bool> resultMessage = await DeleteWithoutTransaction(id);
                if (!resultMessage.IsSuccess)
                    return resultMessage;
                transaction.Commit();
            }
            return new ResultMessage<bool>(true, OperationStatus.Success);
        }

        public async Task<ResultMessage<bool>> DeleteWithoutTransaction(int id)
        {
            ResultMessage<bool> deletedCategoryResult = await _servicesExecutor.Delete((Category x) => x.CategoryId == id && !x.Deleted, x => { x.Deleted = true; return x; });
            if (!deletedCategoryResult.IsSuccess)
                return deletedCategoryResult;
            ResultMessage<bool> deletedStudyMaterials = await _studyMaterialService.DeleteFromCategory(id);
            if (!deletedStudyMaterials.IsSuccess)
                return deletedStudyMaterials;
            return new ResultMessage<bool>(true, OperationStatus.Success);
        }

        public async Task<ResultMessage<CategoryDTO>> GetSingleOrDefault(Predicate<CategoryDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);


        public async Task<ResultMessage<CategoryDTO>> Update(CategoryDTO categoryDTO)
            => await _servicesExecutor.Update(categoryDTO, x => x.CategoryId == categoryDTO.CategoryId && !x.Deleted);
    }
}
