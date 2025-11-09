using DomainLayer.Contracts;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PresistenceLayer.Repositories
{
    public class BasketRepository(IConnectionMultiplexer _connection) : IBasketRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket Basket, TimeSpan? TimeLive = null)
        {
            var JsonData = JsonSerializer.Serialize(Basket);
            var ISCreated = _database.StringSet(Basket.Id, JsonData, TimeLive ?? TimeSpan.FromDays(30));
            
            return await Task.FromResult<CustomerBasket?>(Basket); 
        }

        public async Task<bool> DeleteBasketAsync(string Id)
            => await _database.KeyDeleteAsync(Id);
        

        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            var data =  _database.StringGet(Id);
           
            return JsonSerializer.Deserialize<CustomerBasket>(data!);
        }
    }
}
