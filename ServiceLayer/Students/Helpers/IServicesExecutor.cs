using ServiceLayer.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Helpers
{
    public interface IServicesExecutor<TDto, TEntity> where TEntity : class
    {
        Task<Status> Add(TDto dto, Predicate<TEntity> condition);

        Task<Status> Delete(TEntity entity);

        Task<Status> Update(TDto dto, Predicate<TEntity> condition);

        Task<TDto> GetOne(Predicate<TEntity> condition);

        Task<List<TDto>> GetAll(Predicate<TEntity> condition);

        Task<List<TDto>> GetRange(int offset, int noItems, Predicate<TEntity> condition);

        Task<TEntity> GetSingleOrDefault(Predicate<TEntity> condition);
    }
}
