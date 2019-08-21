using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}
