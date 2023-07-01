using System;
using _2023_MacNETCore_API.Interfaces;
using _2023_MacNETCore_API.Models;
using Microsoft.Extensions.Caching.Memory;

namespace _2023_MacNETCore_API.Caching
{
	public class MemoryCaching : IMemoryCaching
    {
		// Field Properties

        private IMemoryCache _memoryCache;
        private const string managerListCacheKey = "managerList";

        //Constructor

        public MemoryCaching(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        // Methods

        /// <summary>
        /// Checks if data exists in IMemorycache
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Managers> TryGetAllManagersCachedData()
        {
            if (_memoryCache.TryGetValue(managerListCacheKey, out IEnumerable<Managers> managers))
            {
                return managers!;
            }
            else return null!;
        }


        /// <summary>
        /// Caches new Data into the IMemoryCache
        /// </summary>
        /// <param name="managers"></param>
        public void CacheCurrentData(IEnumerable<Managers> managers)
        {
            // Set the iMemory cache options
            var cachedData = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(60))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(60))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(10000);

            // Save the data in Memory Cache
            _memoryCache.Set(managerListCacheKey, managers, cachedData);
        }

    }
}

