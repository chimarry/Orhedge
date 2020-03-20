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
    public class ServiceExecutor<TDto, TEntity> : IServicesExecutor<TDto, TEntity> where TEntity : class where TDto : class
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

        public async Task<TDto> GetSingleOrDefault(Predicate<TDto> condition)
        {
            try
            {
                return await TryGetOne(x => condition(Mapping.Mapper.Map<TDto>(x)));
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex);
                throw ex;
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

        public async Task<List<TDto>> GetAll<TKey>(Predicate<TDto> dtoCondition = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true)
        {
            Predicate<TEntity> entityCondition = null;
            if (dtoCondition != null)
                entityCondition = (TEntity x) => dtoCondition(Mapping.Mapper.Map<TDto>(x));
            return await TryGetAll(entityCondition, sortKeySelector, asc);
        }

        public async Task<List<TDto>> GetRange<TKey>(int offset, int num, Predicate<TDto> dtoCondition = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true)
        {
            Predicate<TEntity> entityCondition = null;
            if (dtoCondition != null)
                entityCondition = (TEntity x) => dtoCondition(Mapping.Mapper.Map<TDto>(x));
            return await TryGetRange(offset, num, entityCondition, sortKeySelector, asc);
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
            Mapping.Mapper.Map(dto, dbEntity);
            await _context.SaveChangesAsync();
            return Status.SUCCESS;
        }

        private async Task<List<TDto>> TryGetAll<TKey>(Predicate<TEntity> filter = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true)
        {
            IQueryable<TEntity> stream;
            // Do filtering
            if (filter == null)
                stream = _context.Set<TEntity>();
            else
                stream = _context.Set<TEntity>().Where(x => filter(x));

            // Do sorting
            if (asc && sortKeySelector != null)
                stream = stream.OrderBy(x => sortKeySelector(Mapping.Mapper.Map<TDto>(x)));
            else if (sortKeySelector != null)
                stream = stream.OrderByDescending(x => sortKeySelector(Mapping.Mapper.Map<TDto>(x)));

            // Do mapping
            return await stream.Select(x => Mapping.Mapper.Map<TDto>(x)).ToListAsync();
        }

        private async Task<List<TDto>> TryGetRange<TKey>(int offset, int noItems, Predicate<TEntity> filter = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true)
        {
            IQueryable<TEntity> stream;

            // Do filtering
            if (filter == null)
                stream = _context.Set<TEntity>();
            else
                stream = _context.Set<TEntity>().Where(x => filter(x));

            // Do sorting
            if (asc && sortKeySelector != null)
                stream = stream.OrderBy(x => sortKeySelector(Mapping.Mapper.Map<TDto>(x)));
            else if (sortKeySelector != null)
                stream = stream.OrderByDescending(x => sortKeySelector(Mapping.Mapper.Map<TDto>(x)));

            // Take range
            stream = stream.Skip(offset).Take(noItems);

            // Do mapping
            return await stream.Select(x => Mapping.Mapper.Map<TDto>(x)).ToListAsync();
        }

        public async Task<int> Count(Predicate<TDto> filter = null)
        {
            IQueryable<TEntity> stream = _context.Set<TEntity>();
            return filter == null ? await stream.CountAsync() : await stream.Where(x => filter(Mapping.Mapper.Map<TDto>(x))).CountAsync();
        }

        #endregion
    }
}
