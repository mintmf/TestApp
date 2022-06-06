using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace TestApp
{
    public class CacheHelper
    {
        public static void SaveToCache(string cacheKey, object savedItem, DateTime absoluteExpiration)
        {
            System.Runtime.Caching.MemoryCache.Default.Add(cacheKey, savedItem, absoluteExpiration);
        }

        public static T GetFromCache<T>(string cacheKey) where T : class
        {
            return System.Runtime.Caching.MemoryCache.Default[cacheKey] as T;
        }

        public static void RemoveFromCache(string cacheKey)
        {
            System.Runtime.Caching.MemoryCache.Default.Remove(cacheKey);
        }

        public static bool IsInCache(string cacheKey)
        {
            return System.Runtime.Caching.MemoryCache.Default[cacheKey] != null;
        }
    }
}
