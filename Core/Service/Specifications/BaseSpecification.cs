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

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
    }
}
