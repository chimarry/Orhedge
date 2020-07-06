using ServiceLayer.DTO.Materials;

namespace ServiceLayer.DTO
{
    public class DetailedCourseCategoryDTO : CourseCategoryDTO
    {
        public int StudyMaterialsCount { get; set; }

        public DetailedCourseCategoryDTO(CourseDTO courseDTO)
        {
            Course = courseDTO;
        }
    }
}
