using ServiceLayer.DTO;
using ServiceLayer.DTO.Materials;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IStudyMaterialManagementService
    {
        Task<HashSet<DetailedSemesterDTO>> GetSemestersWithAllInformation();
        Task<List<CourseCategoryDTO>> GetCoursesByYear(int year);
        Task<ResultMessage<bool>> SaveMaterial(StudyMaterialDTO data, BasicFileInfo basicFileInfo);
        Task<int> Count(int courseId, string searchFor = null, int[] categories = null);
        Task<List<DetailedStudyMaterialDTO>> GetDetailedStudyMaterials<TKey>(int courseId, int offset, int itemsCount, string searchFor = null, int[] categories = null,
                                                                             Func<DetailedStudyMaterialDTO, TKey> sortKeySelector = null, bool asc = true);
    }
}
