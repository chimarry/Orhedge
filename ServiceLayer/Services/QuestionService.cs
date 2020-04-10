using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class QuestionService : BaseService<QuestionDTO, Question>, IQuestionService
    {

        public QuestionService(IServicesExecutor<QuestionDTO, Question> servicesExecutor)
            : base(servicesExecutor) { }

        public Task<ResultMessage<QuestionDTO>> Add(QuestionDTO question)
            => _servicesExecutor.Add(question, x => false);

        public async Task<ResultMessage<bool>> Delete(int id)
           => await _servicesExecutor.Delete((Question x) => x.TopicId == id && !x.Deleted, x => { x.Deleted = true; return x; });

        public async Task<ResultMessage<QuestionDTO>> GetSingleOrDefault(Predicate<QuestionDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);


        public Task<ResultMessage<QuestionDTO>> Update(QuestionDTO question)
            => _servicesExecutor.Update(question, x => x.TopicId == question.TopicId && !x.Deleted);
    }
}
