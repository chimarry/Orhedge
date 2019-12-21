using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Entity
{
    public class Discussion : Topic
    {

        #region NavigationProperties
        public virtual ICollection<DiscussionPost> DiscussionPosts { get; set; }
        #endregion

    }
}
