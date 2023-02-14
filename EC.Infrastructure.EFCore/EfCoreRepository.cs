using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EC.Application.Interfaces;
using EC.Domain.Entities.Base;
using EC.Infrastructure.EFCore.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace EC.Infrastructure.EFCore
{
    public class EfCoreRepository<TEntity, TId> : IRepository<TEntity, TId>, IAsyncRepository<TEntity, TId>
        where TEntity : BaseEntities<TId>
        where TId : IComparable
    {
        private readonly ECContext _context;
        public EfCoreRepository(ECContext context)
        {
            _context = context;
        }
        public TEntity AddEntity(TEntity value)
        {
            _context.Add(value);
            _context.SaveChanges();
            return value;
        }

        public async Task<TEntity> AddEntityAsync(TEntity value)
        {
            _context.Add(value);
            await _context.SaveChangesAsync();
            return value;
        }

        public int Count(IBaseSpecification<TEntity> specification)
        {
            return GetList(specification).Count();
        }

        public async Task<int> CountAsync(IBaseSpecification<TEntity> specification)
        {
            var list = await GetListAsync(specification);
            return list.Count();
        }

        public void DeleteEntity(TEntity value)
        {
            _context.Remove(value);
            _context.SaveChanges();
        }

        public async Task DeleteEntityAsync(TEntity value)
        {
            _context.Remove(value);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<TEntity> GetAllList()
        {
            return _context.Set<TEntity>().ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public TEntity GetById(TId id)
        {
            return _context.Set<TEntity>().FirstOrDefault(q => q.ID.Equals(id));
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(q => q.ID.Equals(id));
        }

        public TEntity GetEntityBySpec(IBaseSpecification<TEntity> specification)
        {
            return OnApplySpecification(specification).FirstOrDefault();
        }

        public async Task<TEntity> GetEntityBySpecAsync(IBaseSpecification<TEntity> specification)
        {
            return await OnApplySpecification(specification).FirstOrDefaultAsync();
        }

        public IEnumerable<TEntity> GetList(IBaseSpecification<TEntity> specification)
        {
            return OnApplySpecification(specification).ToList();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(IBaseSpecification<TEntity> specification)
        {
            return await OnApplySpecification(specification).ToListAsync();
        }

        public TEntity UpdateEntity(TEntity value)
        {
            _context.Update(value);
            _context.SaveChanges();
            return value;
        }

        public async Task<TEntity> UpdateEntityAsync(TEntity value)
        {
            _context.Update(value);
            await _context.SaveChangesAsync();
            return value;
        }

        private IQueryable<TEntity> OnApplySpecification(IBaseSpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity, TId>.GetQuery(_context.Set<TEntity>(), specification);
        }
    }
}
