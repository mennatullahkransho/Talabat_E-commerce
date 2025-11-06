using DomainLayer.Interfaces;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    abstract public class BaseSpecification<TEntity, Tkey> : ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        protected BaseSpecification(Expression<Func<TEntity,bool>>? CriteriaExpression)
        {
            Crirteria = CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>>? Crirteria { get; private set; }

        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
        #endregion

        #region OrderBy
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpr) => OrderBy = orderByExpr;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpr) => OrderByDescending = orderByDescExpr;


        #endregion


    }
}
