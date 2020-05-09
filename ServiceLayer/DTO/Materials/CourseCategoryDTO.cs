using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO.Materials
{
    public class CourseCategoryDTO
    {
        public CourseDTO Course { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}
