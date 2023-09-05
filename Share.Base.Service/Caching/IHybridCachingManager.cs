using EasyCaching.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.Caching
{
    public interface IHybridCachingManager
    {
        IHybridCachingProvider HybridCachingProvider { get; }
    }

    public class HybridCachingManager : IHybridCachingManager
    {
        public IHybridCachingProvider HybridCachingProvider { get; }
        public HybridCachingManager(IHybridProviderFactory hybridFactory)
        {
            HybridCachingProvider = hybridFactory.GetHybridCachingProvider("h1");
        }
    }
}
