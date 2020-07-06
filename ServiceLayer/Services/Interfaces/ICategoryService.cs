using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface ICategoryService : ICRUDServiceTemplate<CategoryDTO>, ISelectableServiceTemplate<CategoryDTO>
    {
        Task<ResultMessage<bool>> DeleteWithoutTransaction(int id);
    }
}
