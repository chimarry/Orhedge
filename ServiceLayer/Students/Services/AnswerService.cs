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
    public class AnswerService : IAnswerService
    {
        private readonly IServicesExecutor<AnswerDTO, Answer> _servicesExecutor;

        public AnswerService(IServicesExecutor<AnswerDTO, Answer> servicesExecutor)
        {
            _servicesExecutor = servicesExecutor;
        }

        public async Task<Status> Add(AnswerDTO answerDTO)
        {
            return await _servicesExecutor.Add(answerDTO, x => false);
        }

        public async Task<Status> Delete(int id)
        {
            Answer dbAnswer = await _servicesExecutor.GetOne(x => x.AnswerId == id && x.Deleted == false);
            if(dbAnswer == null)
            {
                return Status.NOT_FOUND;
            }
            dbAnswer.Deleted = true;
            return await _servicesExecutor.Delete(dbAnswer);
        }

        public async Task<List<AnswerDTO>> GetAll()
        {
            return await _servicesExecutor.GetAll(x => x.Deleted == false);
        }

        public async Task<List<AnswerDTO>> GetAll<TKey>(Func<AnswerDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetAll(x => x.Deleted == false, sortKeySelector, asc);
        }

        public async Task<AnswerDTO> GetById(int id)
        {
            return await _servicesExecutor.GetSingleOrDefault(x => x.AnswerId == id && x.Deleted == false);
        }

        public Task<List<AnswerDTO>> GetRange(int startPosition, int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AnswerDTO>> GetRange<TKey>(int offset, int num, Func<AnswerDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetRange(offset, num, x => x.Deleted == false, sortKeySelector, asc);
        }

        public async Task<AnswerDTO> GetSingleOrDefault(Predicate<AnswerDTO> condition)
        {
            return await _servicesExecutor.GetSingleOrDefault(condition);
        }

        public async Task<Status> Update(AnswerDTO answerDTO)
        {
            return await _servicesExecutor.Update(answerDTO, x => x.AnswerId == answerDTO.AnswerId && x.Deleted == false);
        }
    }
}
