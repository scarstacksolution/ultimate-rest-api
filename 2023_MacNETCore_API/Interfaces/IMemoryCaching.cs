using System;
using _2023_MacNETCore_API.Models;

namespace _2023_MacNETCore_API.Interfaces
{
    public interface IMemoryCaching
    {
        IEnumerable<Managers> TryGetAllManagersCachedData();
        void CacheCurrentData(IEnumerable<Managers> managers);
    }
}

