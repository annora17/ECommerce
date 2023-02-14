using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EC.Application.Interfaces
{
    public interface IBaseSpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        IReadOnlyList<Expression<Func<TEntity, object>>> Includes { get; }
        IReadOnlyList<Expression<Func<TEntity, string>>> IncludeStrings { get; }
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescended { get; }
        int Take { get; }
        int Skip { get; }
        bool PageIsEnabled { get; }
    }
}
