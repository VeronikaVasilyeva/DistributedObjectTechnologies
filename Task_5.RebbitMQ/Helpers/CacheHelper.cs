using Task_5.Models;
using System.Collections.Generic;

namespace Task_5.Helpers
{
    public static class CacheHelper
    {
        private static IDictionary<string, PerformanceMould> Cache { get; set; } = new Dictionary<string, PerformanceMould>();

        public static IDictionary<string, PerformanceMould> Get()
        {
            return Cache;
        }

        public static void Save(string key, PerformanceMould mould)
        {
            if (!Cache.ContainsKey(key))
            {
                Cache.Add(key, null);
            }

            Cache[key] = mould;
        }
    }
}