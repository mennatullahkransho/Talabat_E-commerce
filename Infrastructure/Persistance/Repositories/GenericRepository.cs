using DomainLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class GenericRepository<TEntity, TKey>(TStoreDbContext storeDbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity) => await storeDbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => storeDbContext.Set<TEntity>().Remove(entity);
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await storeDbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id) => await storeDbContext.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity) => storeDbContext.Set<TEntity>().Update(entity);

        #region WithSpecifications
        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationsEvaluator.CreateQuery(storeDbContext.Set<TEntity>(), specification).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification)
        {
            return await SpecificationsEvaluator.CreateQuery(storeDbContext.Set<TEntity>(), specification).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<TEntity, TKey> specification)
        => await SpecificationsEvaluator.CreateQuery(storeDbContext.Set<TEntity>(), specification).CountAsync();

        #endregion
    }
}
