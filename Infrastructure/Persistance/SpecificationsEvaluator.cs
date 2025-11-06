using DomainLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey> (IQueryable<TEntity> InputQuery , ISpecification<TEntity,TKey> specification) where TEntity :BaseEntity<TKey>
        {
            var Query = InputQuery;
            if(specification.Crirteria != null)
            {
                Query = Query.Where(specification.Crirteria);
            }
            if(specification.IncludeExpressions != null && specification.IncludeExpressions.Count > 0)
            {
                Query = specification.IncludeExpressions.Aggregate(Query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            }
            return Query;

        }
    }
}
