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
    public class QuestionService : IQuestionService
    {

        private readonly IServicesExecutor<QuestionDTO, Question> _servicesExecutor;

        public QuestionService(IServicesExecutor<QuestionDTO, Question> servicesExecutor)
            => _servicesExecutor = servicesExecutor;
        public Task<Status> Add(QuestionDTO question)
            => _servicesExecutor.Add(question, x => false);

        public async Task<Status> Delete(int id)
        {
            Question question = await _servicesExecutor.GetOne(x => x.TopicId == id && !x.Deleted);
            question.Deleted = true;
            return await _servicesExecutor.Delete(question);
        }

        public async Task<List<QuestionDTO>> GetAll()
            => await _servicesExecutor.GetAll(x => !x.Deleted);


        public async Task<List<QuestionDTO>> GetAll<TKey>(Func<QuestionDTO, TKey> sortKeySelector, bool asc = true)
            => await _servicesExecutor.GetAll(x => !x.Deleted, sortKeySelector, asc);


        public async Task<QuestionDTO> GetById(int id)
            => await _servicesExecutor.GetSingleOrDefault(x => x.TopicId == id && !x.Deleted);


        public async Task<List<QuestionDTO>> GetRange(int startPosition, int numberOfItems)
            => throw new NotImplementedException();


        public async Task<List<QuestionDTO>> GetRange<TKey>(int offset, int num, Func<QuestionDTO, TKey> sortKeySelector, bool asc = true)
            => await _servicesExecutor.GetRange(offset, num, x => !x.Deleted, sortKeySelector, asc);


        public async Task<QuestionDTO> GetSingleOrDefault(Predicate<QuestionDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);


        public Task<Status> Update(QuestionDTO question)
            => _servicesExecutor.Update(question, x => x.TopicId == question.TopicId && !x.Deleted);
    }
}
