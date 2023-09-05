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
            public static class ProviderNames
            {
                public static readonly string InMemory = "Base:Caching:InMemory";

                public static readonly string Redis = "Base:Caching:Redis";

                public static readonly string Hybrid = "Base:HybridCaching";
            }

            public static class ConfigSectionNames
            {
                public static readonly string InMemory = "Caching:InMemory";

                public static readonly string Redis = "Caching:Redis";

                public static readonly string RedisBus = "Caching:RedisBus";
            }

            public static readonly string CacheName = "Base:Caching";

            public static readonly string HybridTopicName = "Base:Caching:Hybrid";



            public const string WithJson_Name = "myjson";
            public const string InMemory = "localCache";
            public const string Redis = "redisCache";
            public const string TopicName = "hybridCacheTopic";
            public const string HybridName = "hybridCache";
        }
    }
}
