using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Entity
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int AnswerId { get; set; }
        public int StudentId { get; set; }
        public DateTime Created { get; set; }
        public bool Edited { get; set; }
        public bool Deleted { get; set; }
        public string Content { get; set; }

        #region NavigationProperties
        public virtual Answer Answer { get; set; }
        public virtual Student Student { get; set; }
        #endregion

    }
}
