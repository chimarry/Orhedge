using Orhedge.Helpers;
using System.Collections.Generic;

namespace Orhedge.ViewModels.StudyMaterial
{
    public class CourseStudyMaterialsViewModel : PageableViewModel
    {
        public int CourseId { get; set; }

        public List<StudyMaterialViewModel> StudyMaterials { get; set; }

        public CourseStudyMaterialsViewModel(int courseId, List<StudyMaterialViewModel> list, PageInformation pageInformation) : base(pageInformation)
        {
            CourseId = courseId;
            StudyMaterials = list;
        }
    }
}
