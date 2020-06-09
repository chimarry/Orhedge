using ServiceLayer.DTO;
using ServiceLayer.DTO.Materials;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IStudyMaterialManagementService
    {
        Task<HashSet<DetailedSemesterDTO>> GetSemestersWithAllInformation();
        Task<List<CourseCategoryDTO>> GetCoursesByYear(int year);
        Task<ResultMessage<bool>> SaveMaterial(StudyMaterialDTO data, BasicFileInfo basicFileInfo);
    }
}
