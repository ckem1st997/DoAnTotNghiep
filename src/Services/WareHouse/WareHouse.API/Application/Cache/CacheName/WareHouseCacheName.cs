using System.Collections.Generic;

namespace WareHouse.API.Application.Cache.CacheName
{
    public static class WareHouseCacheName
    {
        public const string WareHouseTreeView  = "WareHouse-1-TreeView-{0}";
        public const string WareHouseDropDown  = "WareHouse-1-DropDown-{0}";
        public const string Prefix = "WareHouse-1-";
        public static List<string> Domain { get { return typeof(WareHouseCacheName).GetAllPublicConstantValues<string>(); } }
    }
       public static class WareHouseItemCategoryCacheName
    {
        public const string WareHouseItemCategoryTreeView  = "WareHouseItemCategory-2-TreeView-{0}";
        public const string WareHouseItemCategoryDropDown  = "WareHouseItemCategory-2-DropDown-{0}";
        public const string Prefix = "WareHouseItemCategory-2-";
        public static List<string> Domain { get { return typeof(WareHouseItemCategoryCacheName).GetAllPublicConstantValues<string>(); } }
    }
    
}