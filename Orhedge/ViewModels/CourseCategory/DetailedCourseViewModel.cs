using DatabaseLayer.Enums;
using Orhedge.ViewModels.StudyMaterial;
using System.Collections.Generic;

namespace Orhedge.ViewModels.CourseCategory
{
    public class DetailedCourseViewModel
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public int StudyMaterialsCount { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
