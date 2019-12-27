using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class AnswerRatingService : BaseService<AnswerRatingDTO, AnswerRating>, IAnswerRatingService
    {

        public AnswerRatingService(IServicesExecutor<AnswerRatingDTO, AnswerRating> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(AnswerRatingDTO answerRatingDTO)
        {
            return await _servicesExecutor.Add(answerRatingDTO, x => false);
        }

        public Task<Status> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AnswerRatingDTO>> GetAll()
        {
            return await _servicesExecutor.GetAll(x => true);
        }

        public async Task<List<AnswerRatingDTO>> GetAll<TKey>(Func<AnswerRatingDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetAll(x => true, sortKeySelector, asc);
        }

        public Task<AnswerRatingDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AnswerRatingDTO>> GetRange(int startPosition, int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AnswerRatingDTO>> GetRange<TKey>(int offset, int num, Func<AnswerRatingDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetRange(offset, num, x => true, sortKeySelector, asc);
        }

        public async Task<AnswerRatingDTO> GetSingleOrDefault(Predicate<AnswerRatingDTO> condition)
        {
            return await _servicesExecutor.GetSingleOrDefault(condition);
        }

        public async Task<Status> Update(AnswerRatingDTO answerRatingDTO)
        {
            return await _servicesExecutor.Update(answerRatingDTO, x => x.AnswerId == answerRatingDTO.AnswerId && x.StudentId == answerRatingDTO.StudentId);
        }
    }
}
