using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.Caching.CacheName
{
    public static class CacheHelper
    {
        //WithJson
        public static class CacheConfig
        {
            public const string WithJson_Name = "myjson";
            public const string InMemory = "localCache";
            public const string Redis = "redisCache";
            public const string TopicName = "hybridCacheTopic";
        }
    }
}
