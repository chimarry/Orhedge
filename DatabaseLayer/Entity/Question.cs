using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Entity
{
    public class Question : Topic
    {
        #region NavigationProperties
        public virtual ICollection<Answer> Answers { get; set; }
        #endregion
    }
}
