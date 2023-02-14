using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EC.Application.Specifications;
using EC.Domain.Entities.Base;

namespace EC.Application.Interfaces
{
    public interface IAsyncRepository<TEntity,TId> 
        where TEntity : BaseEntities<TId>
        where TId : IComparable
    {
        Task<TEntity> AddEntityAsync(TEntity value);
        Task<TEntity> UpdateEntityAsync(TEntity value);
        Task DeleteEntityAsync(TEntity value);
        Task<IEnumerable<TEntity>> GetAllListAsync();
        Task<IEnumerable<TEntity>> GetListAsync(IBaseSpecification<TEntity> specification);
        Task<TEntity> GetByIdAsync(TId id);
        Task<TEntity> GetEntityBySpecAsync(IBaseSpecification<TEntity> specification);
        Task<int> CountAsync(IBaseSpecification<TEntity> specification);
    }
}
