namespace DatabaseLayer.Entity
{
    public class StudyMaterialRating
    {
        public int StudyMaterialId { get; set; }
        public int StudentId { get; set; }
        public double Rating { get; set; }

        #region NavigationProperties
        public virtual StudyMaterial StudyMaterial { get; set; }
        public virtual Student Student { get; set; }
        #endregion
    }
}
