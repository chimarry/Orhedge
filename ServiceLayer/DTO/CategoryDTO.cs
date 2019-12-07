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

        public override bool Equals(object obj)
        {
            return obj is CategoryDTO dTO &&
                   CourseId == dTO.CourseId &&
                   Name == dTO.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CourseId, Name);
        }
    }
}
