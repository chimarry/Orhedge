using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AnswerService : BaseService<AnswerDTO, Answer>, IAnswerService
    {
        public AnswerService(IServicesExecutor<AnswerDTO, Answer> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<ResultMessage<AnswerDTO>> Add(AnswerDTO answerDTO)
             => await _servicesExecutor.Add(answerDTO, x => false);

        public async Task<ResultMessage<bool>> Delete(int id)
             => await _servicesExecutor.Delete((Answer x) => x.AnswerId == id && x.Deleted == false, x => { x.Deleted = true; return x; });

        public async Task<ResultMessage<AnswerDTO>> GetSingleOrDefault(Predicate<AnswerDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<ResultMessage<AnswerDTO>> Update(AnswerDTO answerDTO)
            => await _servicesExecutor.Update(answerDTO, x => x.AnswerId == answerDTO.AnswerId && x.Deleted == false);
    }
}
