using AutoMapper;
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
    /// <summary>
    /// Contains logic needed for exectution of CRUD operations, does important lower-higher layer mapping, 
    /// and handles errors that happend while working with database or external library.
    /// </summary>
    /// <typeparam name="TDto">Higher-layer class</typeparam>
    /// <typeparam name="TEntity">Lower-layer class</typeparam>
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
                entity = await GetSingleOrDefault(condition);
                return new ResultMessage<TDto>(Mapping.Mapper.Map<TDto>(entity));
            }
            catch (DbUpdateException ex)
            {
                return new ResultMessage<TDto>(_errorHandler.Handle(ex));
            }
            catch (AutoMapperMappingException ex)
            {
                return new ResultMessage<TDto>(_errorHandler.Handle(ex));
            }
            catch (Exception ex)
            {
                return new ResultMessage<TDto>(_errorHandler.Handle(ex));
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
                if (entity == null)
                    return new ResultMessage<bool>(false, OperationStatus.NotFound);
                entity = applyDelete(entity);
                await _context.SaveChangesAsync();
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (DbUpdateException ex)
            {
                return new ResultMessage<bool>(_errorHandler.Handle(ex));
            }
            catch (Exception ex)
            {
                return new ResultMessage<bool>(_errorHandler.Handle(ex));
            }
        }

        /// <summary>
        /// Returns converted entity that satify condition, or null if there is no such entity.
        /// </summary>
        public async Task<ResultMessage<TDto>> GetSingleOrDefault(Predicate<TDto> condition)
        {
            try
            {
                TEntity entity = await GetSingleOrDefault((TEntity x) => condition(Mapping.Mapper.Map<TDto>(x)));
                if (entity == null)
                    return new ResultMessage<TDto>(OperationStatus.NotFound);
                return new ResultMessage<TDto>(Mapping.Mapper.Map<TDto>(entity), OperationStatus.Success);
            }
            catch (AutoMapperMappingException ex)
            {
                return new ResultMessage<TDto>(_errorHandler.Handle(ex));
            }
            catch (Exception ex)
            {
                return new ResultMessage<TDto>(_errorHandler.Handle(ex));
            }
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
                return new ResultMessage<TDto>(_errorHandler.Handle(ex));
            }
            catch (AutoMapperMappingException ex)
            {
                return new ResultMessage<TDto>(_errorHandler.Handle(ex));
            }
            catch (Exception ex)
            {
                return new ResultMessage<TDto>(_errorHandler.Handle(ex));
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
            try
            {
                IQueryable<TEntity> stream = FilterSort(dtoCondition, sortKeySelector, asc);

                // Do mapping
                return await stream.Select(x => Mapping.Mapper.Map<TDto>(x)).ToListAsync();
            }
            catch (AutoMapperMappingException ex)
            {
                _errorHandler.Handle(ex);
                return new List<TDto>();
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex);
                return new List<TDto>();
            }
        }

        /// <summary>
        /// Returns elements of DbSet in selected range. Based on specified arguments, this list can be
        /// filtered, sorted or both.
        /// </summary>
        /// <typeparam name="TKey">Type of key used for sorting</typeparam>
        public async Task<List<TDto>> GetRange<TKey>(int offset, int num, Predicate<TDto> dtoCondition = null, Func<TDto, TKey> sortKeySelector = null, bool asc = true)
        {
            try
            {

                IQueryable<TEntity> stream = FilterSort(dtoCondition, sortKeySelector, asc);

                // Take range
                stream = stream.Skip(offset).Take(num);

                // Do mapping
                return await stream.Select(x => Mapping.Mapper.Map<TDto>(x)).ToListAsync();
            }
            catch (AutoMapperMappingException ex)
            {
                _errorHandler.Handle(ex);
                return new List<TDto>();
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex);
                return new List<TDto>();
            }
        }

        /// <summary>
        /// Counts elements in one DbSet. It can also count elements that 
        /// satify specific condition.
        /// </summary>
        public async Task<int> Count(Predicate<TDto> filter = null)
        {
            try
            {

                IQueryable<TEntity> stream = _context.Set<TEntity>();
                return filter == null ? await stream.CountAsync() : await stream.Where(x => filter(Mapping.Mapper.Map<TDto>(x))).CountAsync();
            }
            catch (AutoMapperMappingException ex)
            {
                _errorHandler.Handle(ex);
                return Constants.ERROR_INDICATOR;
            }
            catch (Exception ex)
            {
                _errorHandler.Handle(ex);
                return Constants.ERROR_INDICATOR;
            }
        }

        /// <summary>
        /// Saves current changes on context and handles errors.
        /// </summary>
        public async Task<ResultMessage<bool>> SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
                return new ResultMessage<bool>(true, OperationStatus.Success);
            }
            catch (DbUpdateException ex)
            {
                return new ResultMessage<bool>(_errorHandler.Handle(ex));
            }
            catch (Exception ex)
            {
                return new ResultMessage<bool>(_errorHandler.Handle(ex));
            }
        }

        /// <summary>
        /// This function applies filter on collection specified with <see cref="TEntity"/> type, then sorts it in certain direction 
        /// based on specified key.
        /// </summary>
        /// <typeparam name="TKey">Type of key that is used in sorting</typeparam>
        /// <param name="dtoCondition">Filter function that can be applied on correspoding <see cref="TDto"/> collection of objects</param>
        /// <param name="sortKeySelector">Function that gived one property from an <see cref="TEntity"/> object that represents sorting criteria</param>
        /// <param name="asc">If there is sorting, this flag indicates the direction.
        /// If true, than collection will be sorted ascending, else descending</param>
        /// <returns><see cref="IQueryable"/> collection of objects</returns>
        /// <exception cref="AutoMapperMappingException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
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
