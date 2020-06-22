using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.DTO
{
    public class ChatMessageDTO
    {
        public int ChatMessageId { get; set; }

        public string Message { get; set; }

        public int StudentId { get; set; }

        public string Username { get; set; }

        public string StudentInitials { get; set; }

        public bool Deleted { get; set; }

        public DateTime SentOn { get; set; }
    }
}
