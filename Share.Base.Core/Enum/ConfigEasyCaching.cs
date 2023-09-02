using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Core.Enum
{
    public enum ConfigEasyCaching
    {
        InMemoryOnly,
        RedisOnly,
        /// <summary>
        /// using redis and inmemory
        /// </summary>
        Hybrid
    }
}
