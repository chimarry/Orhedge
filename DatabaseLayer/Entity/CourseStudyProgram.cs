using DatabaseLayer.Enums;
using System;

namespace DatabaseLayer.Entity
{
    public class CourseStudyProgram
    {
        public int CourseId { get; set; }

        public StudyProgram StudyProgram { get; set; }

        public Semester Semester { get; set; }

        public int StudyYear { get; set; }

        #region NavigationProperties
        public virtual Course Course { get; set; }
        #endregion
    }
}
