using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class CourseDTO
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public int StudyYear { get; set; }

        public int Semester { get; set; }

        public bool Deleted { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CourseDTO dTO &&
                   Name == dTO.Name &&
                   StudyYear == dTO.StudyYear &&
                   Semester == dTO.Semester;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, StudyYear, Semester);
        }
    }
}
