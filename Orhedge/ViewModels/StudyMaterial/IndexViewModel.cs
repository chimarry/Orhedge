using System.Collections.Generic;

namespace Orhedge.ViewModels.StudyMaterial
{
    public class IndexViewModel
    {
        public CreateStudyMaterialViewModel StudyMaterial { get; set; }

        public List<SemesterViewModel> Semesters { get; set; }
    }
}
