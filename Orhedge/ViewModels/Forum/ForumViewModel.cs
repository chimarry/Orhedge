
namespace Orhedge.ViewModels.Forum
{
    public class ForumViewModel
    {
        public TopicSelectionViewModel[] Discussions { get; set; }
        public int DiscussionPageCount { get; set; }
        public int DiscussionSelectedPage { get; set; }

        public TopicSelectionViewModel[] Questions { get; set; }
        public int QuestionPageCount { get; set; }
        public int QuestionSelectedPage { get; set; }
    }
}
