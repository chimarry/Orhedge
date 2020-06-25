using DatabaseLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.AutoMapper;
using ServiceLayer.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer.Helpers
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

        /// <summary>
        /// Converts DTO object to Entity, and adds it to database, if there is no element that satify condition.
        /// </summary>
        public async Task<ResultMessage<TDto>> Add(TDto dto, Predicate<TEntity> condition)
        {
            try
            {
                TEntity entity = await GetSingleOrDefault(condition);
                if (entity != null)
                    return new ResultMessage<TDto>(OperationStatus.Exists);
                await _context.Set<TEntity>().AddAsync(Mapping.Mapper.Map<TEntity>(dto));
                await _context.SaveChangesAsync();
                return new ResultMessage<TDto>(Mapping.Mapper.Map<TDto>(entity));
            }
            catch (DbUpdateException ex)
            {
                OperationStatus status = _errorHandler.Handle(ex);
                return new ResultMessage<TDto>(status);
            }
        }

        /// <summary>
        /// Saves changes to changed entity (flag Deleted is marked);
        /// </summary>
        public async Task<ResultMessage<bool>> Delete(Predicate<TEntity> filter, Func<TEntity, TEntity> applyDelete)
        {
            try
            {
                TEntity entity = await GetSingleOrDefault(filter);
                entity = applyDelete(entity);
                await _context.SaveChangesAsync();
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (DbUpdateException ex)
            {
                OperationStatus status = _errorHandler.Handle(ex);
                return new ResultMessage<bool>(status);
            }
        }

        /// <summary>
        /// Returns converted entity that satify condition, or null if there is no such entity.
        /// </summary>
        public async Task<ResultMessage<TDto>> GetSingleOrDefault(Predicate<TDto> condition)
        {
            TEntity entity = await GetSingleOrDefault((TEntity x) => condition(Mapping.Mapper.Map<TDto>(x)));
            if (entity == null)
                return new ResultMessage<TDto>(OperationStatus.NotFound);
            return new ResultMessage<TDto>(Mapping.Mapper.Map<TDto>(entity), OperationStatus.Success);
        }

        /// <summary>
        /// Finds entity that satify condition, and updates it based on specified DTO object.
        /// </summary>
        public async Task<ResultMessage<TDto>> Update(TDto dto, Predicate<TEntity> condition)
        {
            try
            {
                TEntity entity = await GetSingleOrDefault(condition);
                if (entity == null)
                    return new ResultMessage<TDto>(OperationStatus.NotFound);
                entity = Mapping.Mapper.Map(dto, entity);
                await _context.SaveChangesAsync();
                return new ResultMessage<TDto>(Mapping.Mapper.Map<TDto>(entity), OperationStatus.Success);
            }
            catch (DbUpdateException ex)
            {
                OperationStatus status = _errorHandler.Handle(ex);
                return new ResultMessage<TDto>(status);
            }
        }

        /// <summary>
        /// Returns entity that satify condition or null.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<TEntity> GetSingleOrDefault(Predicate<TEntity> condition)
        => await _context.Set<TEntity>().SingleOrDefaultAsync(x => condition(x));

        /// <summary>
        /// Returns elements of DbSet. Based on specified arguments, this list can be
        /// filtered, sorted or both.
        /// </summary>
        /// <typeparam name="TKey">Type of key used for sorting</typeparam>
        public async Task<List<TDto>> GetAll<TKey>(Predicate<TDto> dtoCondition = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true)
        {
            IQueryable<TEntity> stream = FilterSort(dtoCondition, sortKeySelector, asc);

            // Do mapping
            return await stream.Select(x => Mapping.Mapper.Map<TDto>(x)).ToListAsync();
        }

        /// <summary>
        /// Returns elements of DbSet in selected range. Based on specified arguments, this list can be
        /// filtered, sorted or both.
        /// </summary>
        /// <typeparam name="TKey">Type of key used for sorting</typeparam>
        public async Task<List<TDto>> GetRange<TKey>(int offset, int num, Predicate<TDto> dtoCondition = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true)
        {
            IQueryable<TEntity> stream = FilterSort(dtoCondition, sortKeySelector, asc);

            // Take range
            stream = stream.Skip(offset).Take(num);

            // Do mapping
            return await stream.Select(x => Mapping.Mapper.Map<TDto>(x)).ToListAsync();
        }

        /// <summary>
        /// Counts elements in one DbSet. It can also count elements that 
        /// satify specific condition.
        /// </summary>
        public async Task<int> Count(Predicate<TDto> filter = null)
        {
            IQueryable<TEntity> stream = _context.Set<TEntity>();
            return filter == null ? await stream.CountAsync() : await stream.Where(x => filter(Mapping.Mapper.Map<TDto>(x))).CountAsync();
        }

        /// <summary>
        /// Saves current changes on context and handles errors.
        /// </summary>
        /// <returns></returns>
        public async Task<ResultMessage<bool>> SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (DbUpdateException ex)
            {
                OperationStatus status = _errorHandler.Handle(ex);
                return new ResultMessage<bool>(false, status);
            }
        }

        private IQueryable<TEntity> FilterSort<TKey>(Predicate<TDto> dtoCondition = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true)
        {
            Predicate<TEntity> entityCondition = null;
            if (dtoCondition != null)
                entityCondition = (TEntity x) => dtoCondition(Mapping.Mapper.Map<TDto>(x));

            IQueryable<TEntity> stream;

            // Do filtering
            if (entityCondition == null)
                stream = _context.Set<TEntity>();
            else
                stream = _context.Set<TEntity>().Where(x => entityCondition(x));

            // Do sorting
            if (asc && sortKeySelector != null)
                stream = stream.OrderBy(x => sortKeySelector(Mapping.Mapper.Map<TDto>(x)));
            else if (sortKeySelector != null)
                stream = stream.OrderByDescending(x => sortKeySelector(Mapping.Mapper.Map<TDto>(x)));
            return stream;
        }
    }
}
