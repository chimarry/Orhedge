using System;

namespace Orhedge.ViewModels.Forum
{
    public class TopicSelectionViewModel
    {
        public string Title { get; set; }
        public DateTime LastPost { get; set; }
        public AuthorViewModel Author { get; set; }
        public int PostCount { get; set; }
    }
}
