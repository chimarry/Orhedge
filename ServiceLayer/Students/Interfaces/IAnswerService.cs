using ServiceLayer.DTO;

namespace ServiceLayer.Students.Interfaces
{
    public interface IAnswerService : ICRUDServiceTemplate<AnswerDTO>, ISelectableServiceTemplate<AnswerDTO>
    {
    }
}
