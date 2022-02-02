using System.Collections.Generic;

namespace WareHouse.API.Application.Cache.CacheName
{
    public static class WareHouseCacheName
    {
        public const string WareHouseTreeView  = "WareHouse-1-TreeView-{0}";
        public const string WareHouseDropDown  = "WareHouse-1-DropDown-{0}";
        public static List<string> Domain { get { return typeof(WareHouseCacheName).GetAllPublicConstantValues<string>(); } }
    }
    
    
}