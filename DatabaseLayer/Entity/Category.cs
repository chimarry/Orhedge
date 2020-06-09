using System.Collections.Generic;

namespace DatabaseLayer.Entity
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }


        #region NavigationProperties
        public virtual Course Course { get; set; }
        public virtual ICollection<StudyMaterial> StudyMaterials { get; set; }
        #endregion
    }
}
