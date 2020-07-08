using ServiceLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public abstract class BaseService<TDto, TEntity> : ISelectableServiceTemplate<TDto> where TEntity : class
    {
        protected readonly IServicesExecutor<TDto, TEntity> _servicesExecutor;

        public BaseService(IServicesExecutor<TDto, TEntity> servicesExecutor)
            => _servicesExecutor = servicesExecutor;

        public async virtual Task<int> Count(Predicate<TDto> filter = null)
            => await _servicesExecutor.Count(filter);

        public async Task<List<TDto>> GetAll<TKey>(Predicate<TDto> condition = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true)
            => await _servicesExecutor.GetAll(condition, sortKeySelector, asc);

        public async Task<List<TDto>> GetRange<TKey>(int offset, int num, Predicate<TDto> condition = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true)
            => await _servicesExecutor.GetRange(offset, num, condition, sortKeySelector, asc);
    }
}
