using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Helpers
{
    public interface IServicesExecutor<TDto, TEntity> where TEntity : class
    {
        Task TryAdding(TDto dto, Predicate<TEntity> condition);
        Task TryDeleting(TEntity entity);
        Task TryUpdating(TDto dto, Predicate<TEntity> condition);
        Task<TDto> TryGettingOne(Predicate<TEntity> condition);
        Task<IList<TDto>> TryGettingAll(Predicate<TEntity> condition);
        Task<TEntity> GetSingleOrDefault(Predicate<TEntity> condition);
    }
}
