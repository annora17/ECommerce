using EC.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EC.Application.Specifications
{
    public abstract class BaseSpecification<TEntity> : IBaseSpecification<TEntity>
    {
        public Expression<Func<TEntity, bool>> Criteria { get; protected set; }

        private List<Expression<Func<TEntity, object>>> includes = new List<Expression<Func<TEntity, object>>>();
        public IReadOnlyList<Expression<Func<TEntity, object>>> Includes => includes.AsReadOnly();

        private List<Expression<Func<TEntity, string>>> includeStrings = new List<Expression<Func<TEntity, string>>>();
        public IReadOnlyList<Expression<Func<TEntity, string>>> IncludeStrings => includeStrings.AsReadOnly();
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescended { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool PageIsEnabled { get; private set; } = false;
        public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        protected virtual void AddIncludes(Expression<Func<TEntity, object>> expression)
        {
            includes.Add(expression);
        }
        protected virtual void AddIncludeStrings(Expression<Func<TEntity, string>> expression)
        {
            includeStrings.Add(expression);
        }
        protected virtual void AddPagination(int take,int skip)
        {
            Take = take;
            Skip = skip;
            PageIsEnabled = true;
        }

        protected virtual void ApplyOrderBy(Expression<Func<TEntity,object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected virtual void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        {
            OrderByDescended = orderByDescExpression;
        }
    }
}
