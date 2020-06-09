namespace DatabaseLayer.Entity
{
    public class AnswerRating
    {
        public int StudentId { get; set; }
        public int AnswerId { get; set; }
        public double Rating { get; set; }

        #region NavigationProperties
        public virtual Student Student { get; set; }
        public virtual Answer Answer { get; set; }
        #endregion
    }
}
