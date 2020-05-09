using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.ViewModels.StudyMaterial
{
    public class CourseCategoryViewModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
