using System.Collections.Generic;

namespace DatabaseLayer.Entity
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }

        #region NavigationProperties
        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<CourseStudyProgram> CourseStudyPrograms { get; set; }
        #endregion
    }
}
