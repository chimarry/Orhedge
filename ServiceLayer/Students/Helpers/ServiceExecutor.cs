using DatabaseLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.AutoMapper;
using ServiceLayer.Enum;
using ServiceLayer.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Students.Helpers
{
    public class ServiceExecutor<TDto, TEntity> : IServicesExecutor<TDto, TEntity> where TEntity : class
    {
        private readonly OrhedgeContext _context;
        public ServiceExecutor(OrhedgeContext context)
        {
            _context = context;
        }
        public async Task TryAdding(TDto dto, Predicate<TEntity> condition)
        {
            TEntity dbEntity = await GetSingleOrDefault(condition);
            if (dbEntity != null)
            {
                throw new ExistsException();
            }
            await _context.Set<TEntity>().AddAsync(Mapping.Mapper.Map<TEntity>(dto));
            await _context.SaveChangesAsync();
        }

        public async Task TryDeleting(TEntity entity)
        {
            if (entity == null)
            {
                throw new ExistsException();
            }
            await _context.SaveChangesAsync();
        }
        public async Task<TEntity> GetSingleOrDefault(Predicate<TEntity> condition)
        {
            return await _context.Set<TEntity>().Where(x => condition(x)).SingleOrDefaultAsync();
        }
        public async Task<IList<TDto>> TryGettingAll(Predicate<TEntity> condition)
        {
            return await _context.Set<TEntity>().Where(x => condition(x)).Select(x => Mapping.Mapper.Map<TDto>(x)).ToListAsync();
        }

        public async Task<TDto> TryGettingOne(Predicate<TEntity> condition)
        {
            var entity = await GetSingleOrDefault(condition);
            if (entity == null)
            {
                throw new NotFoundException();
            }
            return Mapping.Mapper.Map<TDto>(entity);
        }

        public async Task TryUpdating(TDto dto, Predicate<TEntity> condition)
        {
            var dbEntity = await GetSingleOrDefault(condition);
            if (dbEntity == null)
            {
                throw new NotFoundException();
            }
            Mapping.Mapper.Map<TDto, TEntity>(dto, dbEntity);
            await _context.SaveChangesAsync();
        }
    }
}
