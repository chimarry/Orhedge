using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AnswerService : BaseService<AnswerDTO, Answer>, IAnswerService
    {
        public AnswerService(IServicesExecutor<AnswerDTO, Answer> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(AnswerDTO answerDTO)
             => await _servicesExecutor.Add(answerDTO, x => false);

        public async Task<Status> Delete(int id)
        {
            Answer dbAnswer = await _servicesExecutor.GetOne(x => x.AnswerId == id && x.Deleted == false);
            if (dbAnswer == null)
            {
                return Status.NOT_FOUND;
            }
            dbAnswer.Deleted = true;
            return await _servicesExecutor.Delete(dbAnswer);
        }

        public async Task<AnswerDTO> GetSingleOrDefault(Predicate<AnswerDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<Status> Update(AnswerDTO answerDTO)
            => await _servicesExecutor.Update(answerDTO, x => x.AnswerId == answerDTO.AnswerId && x.Deleted == false);
    }
}
