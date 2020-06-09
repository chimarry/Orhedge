namespace DatabaseLayer.Entity
{
    public class TopicRating
    {
        public int TopicId { get; set; }
        public int StudentId { get; set; }
        public double Rating { get; set; }

        #region NavigationProperties
        public virtual Topic Topic { get; set; }
        public virtual Student Student { get; set; }
        #endregion
    }
}
