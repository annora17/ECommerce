using EC.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using EC.Domain.Entities.Base;

namespace EC.Infrastructure.EFCore
{
    public class SpecificationEvaluator<TEntity,TId> 
        where TEntity : BaseEntities<TId>
        where TId : IComparable
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,IBaseSpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            query = specification.Includes.Aggregate(query,
                (current, include) => current.Include(include));

            query = specification.IncludeStrings.Aggregate(query,
                (current, includeString) => current.Include(includeString));

            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);

            if (specification.OrderByDescended != null)
                query = query.OrderByDescending(specification.OrderByDescended);

            if (specification.PageIsEnabled)
                query = query.Skip(specification.Skip).Take(specification.Take);

            return query;
        }
    }
}
