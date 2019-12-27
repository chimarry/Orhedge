using ServiceLayer.AutoMapper;
using ServiceLayer.Students.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Services
{
    public abstract class BaseService<TDto, TEntity> where TEntity : class
    {
        protected readonly IServicesExecutor<TDto, TEntity> _servicesExecutor;
        public BaseService(IServicesExecutor<TDto, TEntity> servicesExecutor)
            => _servicesExecutor = servicesExecutor;

        public async virtual Task<int> Count()
            => await _servicesExecutor.Count();
        public async virtual Task<int> Count(Predicate<TDto> filter)
            => await _servicesExecutor.Count(x => filter(Mapping.Mapper.Map<TDto>(x)));
    }
}
