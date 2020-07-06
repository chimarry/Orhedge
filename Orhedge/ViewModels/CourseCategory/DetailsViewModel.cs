using DatabaseLayer.Enums;
using System.Collections.Generic;

namespace Orhedge.ViewModels.CourseCategory
{
    public class DetailsViewModel
    {
        public DetailedCourseViewModel DetailedCourseViewModel { get; set; } 

        public List<(Semester, StudyProgram)> SemesterAndStudyPrograms { get; set; }
    }
}
