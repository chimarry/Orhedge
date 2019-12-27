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
    public class CommentService : BaseService<CommentDTO, Comment>, ICommentService
    {
        public CommentService(IServicesExecutor<CommentDTO, Comment> servicesExecutor)
            : base(servicesExecutor) { }

        public async Task<Status> Add(CommentDTO commentDTO)
        {
            return await _servicesExecutor.Add(commentDTO, x => false);
        }

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

        public async Task<List<CommentDTO>> GetAll()
        {
            return await _servicesExecutor.GetAll(x => x.Deleted == false);
        }

        public async Task<List<CommentDTO>> GetAll<TKey>(Func<CommentDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetAll(x => x.Deleted == false, sortKeySelector, asc);
        }

        public async Task<CommentDTO> GetById(int id)
        {
            return await _servicesExecutor.GetSingleOrDefault(x => x.CommentId == id && x.Deleted == false);
        }

        public Task<List<CommentDTO>> GetRange(int startPosition, int numberOfItems)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CommentDTO>> GetRange<TKey>(int offset, int num, Func<CommentDTO, TKey> sortKeySelector, bool asc = true)
        {
            return await _servicesExecutor.GetRange(offset, num, x => x.Deleted == false, sortKeySelector, asc);
        }

        public async Task<CommentDTO> GetSingleOrDefault(Predicate<CommentDTO> condition)
        {
            return await _servicesExecutor.GetSingleOrDefault(condition);
        }

        public async Task<Status> Update(CommentDTO commentDTO)
        {
            return await _servicesExecutor.Update(commentDTO, x => x.CommentId == commentDTO.CommentId && x.Deleted == false);
        }
    }
}
