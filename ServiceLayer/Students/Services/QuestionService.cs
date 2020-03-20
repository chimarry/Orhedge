using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Students.Helpers;
using ServiceLayer.Students.Interfaces;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public class QuestionService : BaseService<QuestionDTO, Question>, IQuestionService
    {

        public QuestionService(IServicesExecutor<QuestionDTO, Question> servicesExecutor)
            : base(servicesExecutor) { }
        public Task<Status> Add(QuestionDTO question)
            => _servicesExecutor.Add(question, x => false);

        public async Task<Status> Delete(int id)
        {
            Question question = await _servicesExecutor.GetOne(x => x.TopicId == id && !x.Deleted);
            question.Deleted = true;
            return await _servicesExecutor.Delete(question);
        }

        public async Task<QuestionDTO> GetSingleOrDefault(Predicate<QuestionDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);


        public Task<Status> Update(QuestionDTO question)
            => _servicesExecutor.Update(question, x => x.TopicId == question.TopicId && !x.Deleted);
    }
}
