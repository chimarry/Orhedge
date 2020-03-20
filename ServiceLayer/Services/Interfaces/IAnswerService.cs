using ServiceLayer.DTO;

namespace ServiceLayer.Services
{
    public interface IAnswerService : ICRUDServiceTemplate<AnswerDTO>, ISelectableServiceTemplate<AnswerDTO>
    {
    }
}
