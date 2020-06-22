using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Entity
{
    public class ChatMessage
    {
        public int ChatMessageId { get; set; }

        public string Message { get; set; }

        public int StudentId { get; set; }

        public DateTime SentOn { get; set; }

        public bool Deleted { get; set; }

        #region NavigationProperties
        public virtual Student Student { get; set; }
        #endregion
    }
}
