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


        public async Task<TDto> GetOne(Predicate<TEntity> condition)
        {
            try
            {
                return await TryGetOne(condition);
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

        public async Task<TEntity> GetSingleOrDefault(Predicate<TEntity> condition)
        {
            return await _context.Set<TEntity>().Where(x => condition(x)).SingleOrDefaultAsync();
        }


        #region MethodExecution

        private async Task<Status> TryAdd(TDto dto, Predicate<TEntity> condition)
        {
            TEntity dbEntity = await GetSingleOrDefault(condition);
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

            TEntity entity = await GetSingleOrDefault(condition);
            return Mapping.Mapper.Map<TDto>(entity);
        }

        private async Task<Status> TryUpdate(TDto dto, Predicate<TEntity> condition)
        {

            TEntity dbEntity = await GetSingleOrDefault(condition);
            if (dbEntity == null)
            {
                return Status.NOT_FOUND;
            }
            Mapping.Mapper.Map<TDto, TEntity>(dto, dbEntity);
            await _context.SaveChangesAsync();
            return Status.SUCCESS;
        }

        #endregion
    }
}
