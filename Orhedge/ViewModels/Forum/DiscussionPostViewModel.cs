using System;

namespace Orhedge.ViewModels.Forum
{
    public class DiscussionPostViewModel
    {
        public string Content { get; set; }
        public AuthorViewModel Author { get; set; }
        public DateTime Created { get; set; }
        public bool Edited { get; set; }
    }
}
