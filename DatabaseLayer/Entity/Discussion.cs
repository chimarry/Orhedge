using System.Collections.Generic;

namespace DatabaseLayer.Entity
{
    public class Discussion : Topic
    {

        #region NavigationProperties
        public virtual ICollection<DiscussionPost> DiscussionPosts { get; set; }
        #endregion

    }
}
