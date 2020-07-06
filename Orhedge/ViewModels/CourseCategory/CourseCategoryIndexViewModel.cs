using Orhedge.Helpers;
using System.Collections.Generic;

namespace Orhedge.ViewModels.CourseCategory
{
    public class CourseCategoryIndexViewModel : PageableViewModel
    {
        public List<DetailedCourseViewModel> Courses { get; set; }

        public CourseCategoryIndexViewModel(List<DetailedCourseViewModel> courses, PageInformation pageInformation) : base(pageInformation)
             => (Courses) = (courses);
    }
}
