using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public interface IQuestionService : ICRUDServiceTemplate<QuestionDTO>, ISelectableServiceTemplate<QuestionDTO>
    {
    }
}
