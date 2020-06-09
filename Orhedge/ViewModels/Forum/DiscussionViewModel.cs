using System;

namespace Orhedge.ViewModels.Forum
{
    public class DiscussionViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime LastPost { get; set; }
        public DateTime Created { get; set; }
        public AuthorViewModel Author { get; set; }
        public DiscussionPostViewModel[] DiscussionPosts { get; set; }

    }
}
