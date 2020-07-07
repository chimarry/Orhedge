using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IStudyMaterialService : ICRUDServiceTemplate<StudyMaterialDTO>, ISelectableServiceTemplate<StudyMaterialDTO>
    {
        /// <summary>
        /// Updates total rating of specified study material.
        /// </summary>
        /// <param name="studyMaterialId">Unique identifier for the study material</param>
        /// <param name="rating">New rating</param>
        /// <returns>True if updated, false if not</returns>
        Task<ResultMessage<bool>> UpdateRating(int studyMaterialId, double rating);

        /// <summary>
        /// Deletes study materials from specific category.
        /// </summary>
        /// <param name="categoryId">Unique identifier of a category</param>
        Task<ResultMessage<bool>> DeleteFromCategory(int categoryId);
    }
}
