using System;

namespace DatabaseLayer.Entity
{
    public class DiscussionPost
    {
        public int DiscussionPostId { get; set; }
        public int StudentId { get; set; }
        public int TopicId { get; set; }
        public DateTime Created { get; set; }
        public bool Edited { get; set; }
        public string Content { get; set; }
        public bool Deleted { get; set; }

        #region NavigationProperties
        public virtual Student Student { get; set; }
        public virtual Discussion Discussion { get; set; }
        #endregion

    }
}
