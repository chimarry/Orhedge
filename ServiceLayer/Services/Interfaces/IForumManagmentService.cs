using ServiceLayer.DTO;
using ServiceLayer.DTO.Forum;
using ServiceLayer.ErrorHandling;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface IForumManagmentService
    {
        Task<TopicListDTO> GetDiscussions(int categoryId, int page, int itemsPerPage);
        Task<TopicListDTO> GetQuestions(int categoryId, int page, int itemsPerPage);
        Task<ResultMessage<DiscussionDTO>> AddDiscussion(int forumCategoryId, int studentId, string title, string content);
        Task<ResultMessage<QuestionDTO>> AddQuestion(int forumCategoryId, int studentId, string title, string content);
        Task<ResultMessage<DiscussionDTO>> GetDiscussion(int discussionId);
        Task<DiscussionPostsDTO> GetDiscussionPosts(int discussionId);
        Task<ResultMessage<StudentDTO>> GetAuthor(int studentId);
    }
}
