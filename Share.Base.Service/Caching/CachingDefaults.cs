using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.Caching
{
    /// <summary>
    /// Represents default values related to caching
    /// </summary>
    public static partial class CachingDefaults
    {
        /// <summary>
        /// Gets the default cache time in minutes
        /// </summary>
        public static int CacheTime => 60;

        /// <summary>
        /// Gets or sets the short term cache time in minutes
        /// </summary>
        public static int ShortTermCacheTime => 5;

        public static int DayCacheTime => 1440;

        public static int MonthCacheTime => 43200;

        //public static int YearCacheTime => 518400;

        public static int MaxCacheTime => int.MaxValue;

        /// <summary>
        /// Gets or sets the bundled files cache time in minutes
        /// </summary>
        public static int BundledFilesCacheTime { get; set; } = 120;

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : Entity type name
        /// {1} : Entity id
        /// </remarks>
        public static string EntityByIdCacheKey => "base.{0}.byid-{1}";

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        /// <remarks>
        /// {0} : Entity type name
        /// </remarks>
        public static string EntityPrefix => "base.{0}";
        public static string DataProtectionKeysName = "DataProtectionKeys"; // DataProtection-Keys
    }
}
