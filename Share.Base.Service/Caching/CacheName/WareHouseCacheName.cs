namespace Share.Base.Service.Caching.CacheName
{
    public static class CacheName
    {
        public const string All = "cache-";
    }

    public static class WareHouseCacheName
    {
        public const string WareHouseTreeView = CacheName.All + "WareHouse-1-TreeView-{0}";
        public const string WareHouseDropDown = CacheName.All + "WareHouse-1-DropDown-{0}";
        public const string WareHouseGetAll = CacheName.All + "WareHouse-1-GetAll-{0}";
        public const string Prefix = CacheName.All + "WareHouse-1-";
    }
    public static class WareHouseItemCategoryCacheName
    {
        public const string WareHouseItemCategoryTreeView = CacheName.All + "WareHouseItemCategory-2-TreeView-{0}";
        public const string WareHouseItemCategoryDropDown = CacheName.All + "WareHouseItemCategory-2-DropDown-{0}";
        public const string Prefix = CacheName.All + "WareHouseItemCategory-2-";
    }


    public static class WareHouseItemCacheName
    {
        public const string WareHouseItemCacheNameTreeView = CacheName.All + "WareHouseItem-3-TreeView-{0}";
        public const string WareHouseItemCacheNameDropDown = CacheName.All + "WareHouseItem-3-DropDown-{0}";
        public const string Prefix = CacheName.All + "WareHouseItem-3-";
    }

    public static class UnitCacheName
    {
        public const string UnitCacheNameTreeView = CacheName.All + "Unit-4-TreeView-{0}";
        public const string UnitCacheNameDropDown = CacheName.All + "Unit-4-DropDown-{0}";
        public const string Prefix = CacheName.All + "Unit-4-";
    }
    public static class VendorCacheName
    {
        public const string VendorCacheNameTreeView = CacheName.All + "Vendor-5-TreeView-{0}";
        public const string VendorCacheNameDropDown = CacheName.All + "Vendor-5-DropDown-{0}";
        public const string Prefix = CacheName.All + "Vendor-5-";
    }

    public static class UserListRoleCacheName
    {
        public const string UserListRoleCache = CacheName.All + "UserListRole-{0}";
        public const string Prefix = CacheName.All + "UserListRole-";
    }

    public static class ListRoleCacheName
    {
        public const string UserListRoleCache = CacheName.All + "ListRole-{0}";
        public const string Prefix = CacheName.All + "ListRole-";
    }


}