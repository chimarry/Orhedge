using DatabaseLayer.Enums;
using System;

namespace Orhedge.ViewModels.TechnicalSupport
{
    public class ChatMessageViewModel
    {
        public int ChatMessageId { get; set; }

        public DateTime SentOn { get; set; }

        public int StudentId { get; set; }

        public StudentPrivilege Privilege { get; set; }

        public string Message { get; set; }

        public string Username { get; set; }

        public string StudentInitials { get; set; }
    }
}
