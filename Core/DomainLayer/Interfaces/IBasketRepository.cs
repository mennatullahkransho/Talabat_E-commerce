using DomainLayer.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Interfaces
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string key);
        public Task<CustomerBasket?> UpdateOrCreateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive=null);
        public Task<bool> DeleteBasketAsync(string key);
    }
}
