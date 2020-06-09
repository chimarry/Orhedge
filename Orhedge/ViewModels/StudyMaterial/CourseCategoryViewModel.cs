using System.Collections.Generic;

namespace Orhedge.ViewModels.StudyMaterial
{
    public class CourseCategoryViewModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
