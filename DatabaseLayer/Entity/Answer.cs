using System;
using System.Collections.Generic;

namespace DatabaseLayer.Entity
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int TopicId { get; set; }
        public int StudentId { get; set; }
        public DateTime Created { get; set; }
        public bool Deleted { get; set; }
        public bool Edited { get; set; }
        public string Content { get; set; }
        public bool BestAnswer { get; set; }

        #region NavigationProperties
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Question Question { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<AnswerRating> AnswerRatings { get; set; }
        #endregion

    }
}
