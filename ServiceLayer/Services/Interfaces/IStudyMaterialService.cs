using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IStudyMaterialService : ICRUDServiceTemplate<StudyMaterialDTO>, ISelectableServiceTemplate<StudyMaterialDTO>
    {
        Task<ResultMessage<bool>> ChangeRating(int studyMaterialId, double rating);
        Task<ResultMessage<bool>> DeleteFromCategory(int categoryId);
    }
}
