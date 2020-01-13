using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class CourseDTO
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Semester { get; set; }

        public bool Deleted { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CourseDTO dTO &&
                   Name == dTO.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}
