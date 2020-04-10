using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AnswerRatingService : BaseService<AnswerRatingDTO, AnswerRating>, IAnswerRatingService
    {

        public AnswerRatingService(IServicesExecutor<AnswerRatingDTO, AnswerRating> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<ResultMessage<AnswerRatingDTO>> Add(AnswerRatingDTO answerRatingDTO)
             => await _servicesExecutor.Add(answerRatingDTO, x => false);

        public Task<ResultMessage<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultMessage<AnswerRatingDTO>> Update(AnswerRatingDTO answerRatingDTO)
             => await _servicesExecutor.Update(answerRatingDTO, x => x.AnswerId == answerRatingDTO.AnswerId && x.StudentId == answerRatingDTO.StudentId);

        public async Task<ResultMessage<AnswerRatingDTO>> GetSingleOrDefault(Predicate<AnswerRatingDTO> condition)
             => await _servicesExecutor.GetSingleOrDefault(condition);
    }
}
