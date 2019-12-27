using DatabaseLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.AutoMapper;
using ServiceLayer.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Helpers
{
    public class ServiceExecutor<TDto, TEntity> : IServicesExecutor<TDto, TEntity> where TEntity : class
    {
        private readonly OrhedgeContext _context;
        private readonly IErrorHandler _errorHandler;
        public ServiceExecutor(OrhedgeContext context, IErrorHandler errorHandler)
        {
            _context = context;
            _errorHandler = errorHandler;
        }

        public async Task<Status> Add(TDto dto, Predicate<TEntity> condition)
        {
            try
            {
                return await TryAdd(dto, condition);
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex);
                return Status.DATABASE_ERROR;
            }
        }

        public async Task<Status> Delete(TEntity entity)
        {
            try
            {
                return await TryDelete(entity);
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex);
                return Status.DATABASE_ERROR;
            }
        }

        public async Task<List<TDto>> GetAll(Predicate<TEntity> condition)
        {
            try
            {
                return await TryGetAll(condition);
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex);
                return new List<TDto>();
            }
        }
        public async Task<List<TDto>> GetRange(int offset, int noItems, Predicate<TEntity> condition)
        {
            try
            {
                return await TryGetRange(offset, noItems, condition);
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex);
                return new List<TDto>();
            }

        }


        public async Task<TDto> GetSingleOrDefault(Predicate<TDto> condition)
        {
            try
            {
                return await TryGetOne(x => condition(Mapping.Mapper.Map<TDto>(x)));
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex);
                return default;
            }
        }

        public async Task<Status> Update(TDto dto, Predicate<TEntity> condition)
        {
            try
            {
                return await TryUpdate(dto, condition);
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex);
                return Status.DATABASE_ERROR;
            }
        }

        public async Task<TEntity> GetOne(Predicate<TEntity> condition)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(x => condition(x));
        }

        public async Task<List<TDto>> GetAll<TKey>(Predicate<TEntity> condition, Func<TDto, TKey> sortKeySelector, bool asc = true)
        {
            return await TryGetAll<TKey>(condition, sortKeySelector, asc);
        }

        public async Task<List<TDto>> GetRange<TKey>(int offset, int num, Predicate<TEntity> filter, Func<TDto, TKey> sortKeySelector, bool asc = true)
        {
            return await TryGetRange<TKey>(offset, num, filter, sortKeySelector, asc);
        }


        #region MethodExecution

        private async Task<Status> TryAdd(TDto dto, Predicate<TEntity> condition)
        {
            TEntity dbEntity = await GetOne(condition);
            if (dbEntity != null)
            {
                return Status.ALREADY_EXISTS;
            }
            await _context.Set<TEntity>().AddAsync(Mapping.Mapper.Map<TEntity>(dto));
            await _context.SaveChangesAsync();
            return Status.SUCCESS;
        }

        private async Task<Status> TryDelete(TEntity entity)
        {
            await _context.SaveChangesAsync();
            return Status.SUCCESS;
        }

        private async Task<List<TDto>> TryGetAll(Predicate<TEntity> condition)
        {

            return await _context.Set<TEntity>().Where(x => condition(x)).Select(x => Mapping.Mapper.Map<TDto>(x)).ToListAsync();

        }

        private async Task<List<TDto>> TryGetRange(int offset, int noItems, Predicate<TEntity> condition)
        {
            return await _context.Set<TEntity>().Where(x => condition(x)).Skip(offset).Take(noItems).Select(x => Mapping.Mapper.Map<TDto>(x)).ToListAsync();
        }

        private async Task<TDto> TryGetOne(Predicate<TEntity> condition)
        {

            TEntity entity = await GetOne(condition);
            return Mapping.Mapper.Map<TDto>(entity);
        }

        private async Task<Status> TryUpdate(TDto dto, Predicate<TEntity> condition)
        {

            TEntity dbEntity = await GetOne(condition);
            if (dbEntity == null)
            {
                return Status.NOT_FOUND;
            }
            Mapping.Mapper.Map<TDto, TEntity>(dto, dbEntity);
            await _context.SaveChangesAsync();
            return Status.SUCCESS;
        }

        private async Task<List<TDto>> TryGetAll<TKey>(Predicate<TEntity> filter, Func<TDto, TKey> sortKeySelector, bool asc = true)
        {
            IQueryable<TEntity> stream = _context.Set<TEntity>().Where(x => filter(x));
            if (asc)
            {
                stream = stream.OrderBy(x => sortKeySelector(Mapping.Mapper.Map<TDto>(x)));
            }
            else
            {
                stream = stream.OrderByDescending(x => sortKeySelector(Mapping.Mapper.Map<TDto>(x)));
            }
            return await stream.Select(x => Mapping.Mapper.Map<TDto>(x)).ToListAsync();
        }

        private async Task<List<TDto>> TryGetRange<TKey>(int offset, int noItems, Predicate<TEntity> filter, Func<TDto, TKey> sortKeySelector, bool asc = true)
        {
            IQueryable<TEntity> stream = _context.Set<TEntity>().Where(x => filter(x));
            if (asc)
            {
                stream = stream.OrderBy(x => sortKeySelector(Mapping.Mapper.Map<TDto>(x)));
            }
            else
            {
                stream = stream.OrderByDescending(x => sortKeySelector(Mapping.Mapper.Map<TDto>(x)));
            }
            stream = stream.Skip(offset).Take(noItems);
            return await stream.Select(x => Mapping.Mapper.Map<TDto>(x)).ToListAsync();
        }

        public async Task<int> Count()
            => await _context.Set<TEntity>().CountAsync();

        public async Task<int> Count(Predicate<TEntity> filter)
            => await _context.Set<TEntity>().Where(x => filter(x)).CountAsync();
        
        #endregion
    }
}
