using DomainLayer.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresistenceLayer.Repositories
{
    public class CacheRepository(IConnectionMultiplexer Connection) : ICacheRepository
    {
        private readonly IDatabase _database = Connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var Cachedvalue = await _database.StringGetAsync(key);
            return Cachedvalue.IsNullOrEmpty ? null : Cachedvalue.ToString();
        }

        public async Task SetAsync(string Cachekey, string Cachevalue, TimeSpan expiration)
        {
             await _database.StringSetAsync(Cachekey, Cachevalue, expiration);
        }
    }
}
