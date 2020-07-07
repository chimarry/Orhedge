using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface ICategoryService : ICRUDServiceTemplate<CategoryDTO>, ISelectableServiceTemplate<CategoryDTO>
    {
        /// <summary>
        /// Deletes category and related study materials, but not in a transaction scope.
        /// </summary>
        /// <param name="categoryId">Unique identifier of an category</param>
        /// <returns>True if deleted, false if not</returns>
        Task<ResultMessage<bool>> DeleteWithoutTransaction(int categoryId);
    }
}
