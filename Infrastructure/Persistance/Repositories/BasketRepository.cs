using DomainLayer.Interfaces;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string key)=> await _database.KeyDeleteAsync(key);


        public async Task<CustomerBasket?> GetBasketAsync(string key)
        {
           var Basket =await _database.StringGetAsync(key);
            if (Basket.IsNullOrEmpty)
                return null;
            return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }

        public async Task<CustomerBasket?> UpdateOrCreateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var jasonBasket = JsonSerializer.Serialize(basket);
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jasonBasket, TimeToLive?? TimeSpan.FromDays(30));
            if (IsCreatedOrUpdated)
                return await GetBasketAsync(basket.Id);
            else
                return null;
        }
    }
}
