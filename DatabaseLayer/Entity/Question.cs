using System.Collections.Generic;

namespace DatabaseLayer.Entity
{
    public class Question : Topic
    {
        #region NavigationProperties
        public virtual ICollection<Answer> Answers { get; set; }
        #endregion
    }
}
