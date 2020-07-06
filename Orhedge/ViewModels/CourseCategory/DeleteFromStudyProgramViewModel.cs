using DatabaseLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orhedge.ViewModels.CourseCategory
{
    public class DeleteFromStudyProgramViewModel
    {
        public Semester Semester { get; set; }

        public StudyProgram StudyProgram { get; set; }

        public int CourseId { get; set; }
    }
}
