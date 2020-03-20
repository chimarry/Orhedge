using ServiceLayer.DTO;

namespace ServiceLayer.Students.Interfaces
{
    public interface ICommentService : ICRUDServiceTemplate<CommentDTO>, ISelectableServiceTemplate<CommentDTO>
    {
    }
}
