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

        public async Task<Status> Add(CommentDTO commentDTO)
           => await _servicesExecutor.Add(commentDTO, x => false);

        public async Task<Status> Delete(int id)
        {
            Comment dbComment = await _servicesExecutor.GetOne(x => x.CommentId == id && x.Deleted == false);
            if (dbComment == null)
            {
                return Status.NOT_FOUND;
            }
            dbComment.Deleted = true;
            return await _servicesExecutor.Delete(dbComment);
        }

        public async Task<CommentDTO> GetSingleOrDefault(Predicate<CommentDTO> condition)
            => await _servicesExecutor.GetSingleOrDefault(condition);

        public async Task<Status> Update(CommentDTO commentDTO)
            => await _servicesExecutor.Update(commentDTO, x => x.CommentId == commentDTO.CommentId && x.Deleted == false);
    }
}
