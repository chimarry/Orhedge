﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Entity
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int StudyYear { get; set; }
        public int Semester { get; set; }
        public bool Deleted { get; set; }

        #region NavigationProperties
        public virtual ICollection<Category> Categories { get; set; } 
        #endregion
    }
}