using System.Collections.Generic;
using EC.Domain.Entities.Base;
using System.Text;
using System;
using EC.Application.Specifications;

namespace EC.Application.Interfaces
{
    public interface IRepository<TEntity,TId> 
        where TEntity : BaseEntities<TId>
        where TId : IComparable
    {
        TEntity AddEntity(TEntity value);
        TEntity UpdateEntity(TEntity value);
        void DeleteEntity(TEntity value);
        IEnumerable<TEntity> GetAllList();
        IEnumerable<TEntity> GetList(IBaseSpecification<TEntity> specification);
        TEntity GetById(TId id);
        TEntity GetEntityBySpec(IBaseSpecification<TEntity> specification);
        int Count(IBaseSpecification<TEntity> specification);
    }
}
