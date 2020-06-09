using System;
using System.Collections.Generic;

namespace DatabaseLayer.Entity
{
    public class Topic
    {
        public int TopicId { get; set; }
        public int StudentId { get; set; }
        public string Title { get; set; }
        public int ForumCategoryId { get; set; }
        public DateTime Created { get; set; }
        public bool Locked { get; set; }
        public bool Deleted { get; set; }
        public string Content { get; set; }
        public DateTime LastPost { get; set; }

        #region NavigationProperties
        public virtual Student Student { get; set; }
        public virtual ForumCategory ForumCategory { get; set; }
        public virtual ICollection<TopicRating> TopicRatings { get; set; }
        #endregion
    }
}
