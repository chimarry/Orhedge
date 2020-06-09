using System.Collections.Generic;

namespace DatabaseLayer.Entity
{
    public class ForumCategory
    {
        public int ForumCategoryId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        #region NavigationProperties
        public virtual ICollection<Topic> Topics { get; set; }
        #endregion
    }
}
