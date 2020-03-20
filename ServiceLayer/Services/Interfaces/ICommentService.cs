using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public interface ICommentService : ICRUDServiceTemplate<CommentDTO>, ISelectableServiceTemplate<CommentDTO>
    {
    }
}
