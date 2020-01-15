using ServiceLayer.DTO.Forum;
using ServiceLayer.ErrorHandling;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Interfaces.Forum
{
    public interface IForumManagmentService
    {
        Task<TopicListDTO> GetDiscussions(int categoryId, int page, int itemsPerPage);
        Task<TopicListDTO> GetQuestions(int categoryId, int page, int itemsPerPage);
        Task<Status> AddDiscussion(int forumCategoryId, int studentId, string title, string content);
    }
}
