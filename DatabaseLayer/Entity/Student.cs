using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DatabaseLayer.Entity
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Index { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public double Rating { get; set; }
        public int Privilege { get; set; }
        public bool Deleted { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        #region NavigationProperties
        public virtual ICollection<StudyMaterial> StudyMaterials { get; set; }
        [InverseProperty("Student")]
        public virtual ICollection<StudyMaterialRating> StudyMaterialRatingsStudents { get; set; }
        #endregion
    }
}
