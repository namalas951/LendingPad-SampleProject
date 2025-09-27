using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface ICustomCache : IMemoryCache
    {
        // You can add custom methods if needed
    }

    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class CustomMemoryCache : MemoryCache, ICustomCache
    {
        public CustomMemoryCache() : base(new MemoryCacheOptions())
        {
        }
        // Optionally, add custom logic here
    }
}
