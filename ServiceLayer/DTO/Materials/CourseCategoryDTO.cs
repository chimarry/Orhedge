using System.Collections.Generic;

namespace ServiceLayer.DTO.Materials
{
    public class CourseCategoryDTO
    {
        public CourseDTO Course { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}
