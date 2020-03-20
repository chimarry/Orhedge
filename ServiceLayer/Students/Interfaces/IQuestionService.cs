using ServiceLayer.DTO;

namespace ServiceLayer.Students.Interfaces
{
    public interface IQuestionService : ICRUDServiceTemplate<QuestionDTO>, ISelectableServiceTemplate<QuestionDTO>
    {
    }
}
