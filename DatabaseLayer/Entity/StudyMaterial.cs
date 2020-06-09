using System;
using System.Collections.Generic;

namespace DatabaseLayer.Entity
{
    public class StudyMaterial
    {
        public int StudyMaterialId { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public DateTime UploadDate { get; set; }
        public int StudentId { get; set; }
        public int CategoryId { get; set; }

        public bool Deleted { get; set; }
        #region NavigationProperies
        public virtual Student Student { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<StudyMaterialRating> StudyMaterialRatings { get; set; }
        #endregion
    }
}
