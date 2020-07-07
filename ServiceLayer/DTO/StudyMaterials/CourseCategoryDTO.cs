using System;
using System.Collections.Generic;

namespace ServiceLayer.DTO.Materials
{
    public class CourseCategoryDTO
    {
        public CourseDTO Course { get; set; }
        public List<CategoryDTO> Categories { get; set; }

        /// <summary>
        /// Treats two objects of this class as the same, 
        /// if they reference the same course. 
        /// If you want different behavior, use IEqualityComparer instead.
        /// </summary>
        public override bool Equals(object obj)
         => obj is CourseCategoryDTO dto && Course.Equals(dto.Course);

        public override int GetHashCode()
         => Course.GetHashCode();
    }
}
