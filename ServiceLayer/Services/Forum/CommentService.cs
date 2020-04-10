using DatabaseLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.ErrorHandling;
using ServiceLayer.Helpers;
using ServiceLayer.Services;
using System;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class CommentService : BaseService<CommentDTO, Comment>, ICommentService
    {
        public CommentService(IServicesExecutor<CommentDTO, Comment> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<ResultMessage<CommentDTO>> Add(CommentDTO commentDTO)
           => await _servicesExecutor.Add(commentDTO, x => false);

        public async Task<ResultMessage<bool>> Delete(int id)
             => await _servicesExecutor.Delete((Comment x) => x.CommentId == id && !x.Deleted, x => { x.Deleted = true; return x; });

        public async Task<ResultMessage<CommentDTO>> GetSingleOrDefault(Predicate<CommentDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<ResultMessage<CommentDTO>> Update(CommentDTO commentDTO)
            => await _servicesExecutor.Update(commentDTO, x => x.CommentId == commentDTO.CommentId && x.Deleted == false);
    }
}
