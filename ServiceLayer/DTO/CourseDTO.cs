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

    }
}
