using DatabaseLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.ViewModels.CourseCategory
{
    public class SaveCourseViewModel
    {
        public string Name { get; set; }
        public Semester Semester { get; set; }

        public StudyProgram StudyProgram { get; set; }
        public string[] Categories { get; set; } = new string[1];

        public string[] GetCategories() => Categories.Where(x => !string.IsNullOrEmpty(x)).ToArray();
    }
}
