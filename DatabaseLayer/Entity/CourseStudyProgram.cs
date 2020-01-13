using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Entity
{
    public class CourseStudyProgram
    {
        public int CourseId { get; set; }

        public int StudyProgramId { get; set; }

        #region NavigationProperties
        public virtual Course Course { get; set; }

        public virtual StudyProgram StudyProgram { get; set; }
        #endregion
    }
}
