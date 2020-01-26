using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Entity
{
    public class StudyProgram
    {
        public int StudyProgramId { get; set; }

        public string Name { get; set; }

        public int Rank { get; set; }

        #region NavigationProperties
        public virtual ICollection<CourseStudyProgram> CourseStudyPrograms { get; set; }
        #endregion
    }
}
