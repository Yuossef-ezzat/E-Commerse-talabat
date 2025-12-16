using DomainLayer.Contracts;
using ServiceAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ServicesLayer.Services
{
    public class CacheService (ICacheRepository _cacheRepository ) : ICacheService
    {
        public async Task<string?> GetAsync(string key)
        {
            return await _cacheRepository.GetAsync(key);
        }

        public async Task SetAsync(string Cachekey, object Cachevalue, TimeSpan expiration)
        {
            var CacheValueString = JsonSerializer.Serialize(Cachevalue);
            await _cacheRepository.SetAsync(Cachekey, CacheValueString, expiration); 
        }
    }
}
