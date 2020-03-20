using ServiceLayer.DTO;

namespace ServiceLayer.Students.Interfaces
{
    public interface IAnswerRatingService : ICRUDServiceTemplate<AnswerRatingDTO>, ISelectableServiceTemplate<AnswerRatingDTO>
    {
    }
}
