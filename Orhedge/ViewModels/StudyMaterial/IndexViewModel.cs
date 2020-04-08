using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.ViewModels.StudyMaterial
{
    public class IndexViewModel
    {
        public CreateStudyMaterialViewModel StudyMaterial { get; set; }

        public List<SemesterViewModel> Semesters { get; set; }
    }
}
