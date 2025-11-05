using DomainLayer.Interfaces;
using DomainLayer.Models;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class UnitOfWork (TStoreDbContext storeDbContext): IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.TryGetValue(typeName, out object? value))
            {
                return (IGenericRepository<TEntity, TKey>)value;
            }
            else { 
                var Repo = new GenericRepository<TEntity, TKey>(storeDbContext);
                _repositories["typeName"] = Repo;
                return Repo;

            }
           
            
        }

        public async Task<int> SaveChangesAsync()=> await storeDbContext.SaveChangesAsync();
    }
}
