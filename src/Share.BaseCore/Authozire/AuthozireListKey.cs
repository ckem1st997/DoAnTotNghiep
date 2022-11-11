using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Authozire
{
    public static class AuthozireListKey
    {
        public static class GlobalKey
        {
            public static class GlobalCreateKey
            {
                public const string Global = "Global.Create";
            }
            public static class GlobalEditKey
            {
                public const string Global = "Global.Edit";
            }
            public static class GlobalDeleteKey
            {
                public const string Global = "Global.Delete";
            }
            public static class GlobalReadKey
            {
                public const string Global = "Global.Read";
            }
        }
        public static class WarehouseKey
        {
            public static class WarehouseCreateKey
            {
                public const string Warehouse = "Warehouse.Warehouse.Create";
                public const string Unit = "Warehouse.Unit.Create";
                public const string WareHouseItem = "Warehouse.WareHouseItem.Create";
                public const string WareHouseItemCategory = "Warehouse.WareHouseItemCategory.Create";
                public const string Vendor = "Warehouse.Vendor.Create";
                public const string Inward = "Warehouse.Inward.Create";
                public const string InwardDetails = "Warehouse.InwardDetails.Create";
                public const string Outward = "Warehouse.Outward.Create";
                public const string OutwardDetails = "Warehouse.OutwardDetails.Create";
                public const string WareHouseBook = "Warehouse.WareHouseBook.Create";
            }
            public static class WarehouseEditKey
            {
                public const string Warehouse = "Warehouse.Warehouse.Edit";
                public const string Unit = "Warehouse.Unit.Edit";
                public const string WareHouseItem = "Warehouse.WareHouseItem.Edit";
                public const string WareHouseItemCategory = "Warehouse.WareHouseItemCategory.Edit";
                public const string Vendor = "Warehouse.Vendor.Edit";
                public const string Inward = "Warehouse.Inward.Edit";
                public const string InwardDetails = "Warehouse.InwardDetails.Edit";
                public const string Outward = "Warehouse.Outward.Edit";
                public const string OutwardDetails = "Warehouse.OutwardDetails.Edit";
                public const string WareHouseBook = "Warehouse.WareHouseBook.Edit";
            }
            public static class WarehouseDeleteKey
            {
                public const string Warehouse = "Warehouse.Warehouse.Delete";
                public const string Unit = "Warehouse.Unit.Delete";
                public const string WareHouseItem = "Warehouse.WareHouseItem.Delete";
                public const string WareHouseItemCategory = "Warehouse.WareHouseItemCategory.Delete";
                public const string Vendor = "Warehouse.Vendor.Delete";
                public const string Inward = "Warehouse.Inward.Delete";
                public const string InwardDetails = "Warehouse.InwardDetails.Delete";
                public const string Outward = "Warehouse.Outward.Create";
                public const string OutwardDetails = "Warehouse.OutwardDetails.Delete";
                public const string WareHouseBook = "Warehouse.WareHouseBook.Delete";
            }
            public static class WarehouseReadKey
            {
                public const string Warehouse = "Warehouse.Warehouse.Read";
                public const string Unit = "Warehouse.Unit.Read";
                public const string WareHouseItem = "Warehouse.WareHouseItem.Read";
                public const string WareHouseItemCategory = "Warehouse.WareHouseItemCategory.Read";
                public const string Vendor = "Warehouse.Vendor.Read";
                public const string Inward = "Warehouse.Inward.Read";
                public const string InwardDetails = "Warehouse.InwardDetails.Read";
                public const string Outward = "Warehouse.Outward.Read";
                public const string OutwardDetails = "Warehouse.OutwardDetails.Read";
                public const string WareHouseBook = "Warehouse.WareHouseBook.Read";
            }
        }
        public static class MasterKey
        {
            public static class MasterCreateKey
            {
                public const string Master = "Master.Master.Create";
            }
            public static class MasterEditKey
            {
                public const string Master = "Master.Master.Edit";

            }
            public static class MasterDeleteKey
            {
                public const string Master = "Master.Master.Delete";

            }
            public static class MasterReadKey
            {
                public const string Master = "Master.Master.Read";

            }
        }
        // name biến truyền qua hàm khơir tạo
        public const string ParameterName = "role";
        public static List<string> GetAllKey()
        {
            List<string> strings = new List<string>();
            strings.AddRange(typeof(AuthozireListKey.WarehouseKey.WarehouseCreateKey).GetAllPublicConstantValues<string>());
            strings.AddRange(typeof(AuthozireListKey.WarehouseKey.WarehouseEditKey).GetAllPublicConstantValues<string>());
            strings.AddRange(typeof(AuthozireListKey.WarehouseKey.WarehouseDeleteKey).GetAllPublicConstantValues<string>());
            strings.AddRange(typeof(AuthozireListKey.WarehouseKey.WarehouseReadKey).GetAllPublicConstantValues<string>());
            //
            strings.AddRange(typeof(AuthozireListKey.MasterKey.MasterCreateKey).GetAllPublicConstantValues<string>());
            strings.AddRange(typeof(AuthozireListKey.MasterKey.MasterEditKey).GetAllPublicConstantValues<string>());
            strings.AddRange(typeof(AuthozireListKey.MasterKey.MasterDeleteKey).GetAllPublicConstantValues<string>());
            strings.AddRange(typeof(AuthozireListKey.MasterKey.MasterReadKey).GetAllPublicConstantValues<string>());
            //
            strings.AddRange(typeof(AuthozireListKey.GlobalKey.GlobalCreateKey).GetAllPublicConstantValues<string>());
            strings.AddRange(typeof(AuthozireListKey.GlobalKey.GlobalDeleteKey).GetAllPublicConstantValues<string>());
            strings.AddRange(typeof(AuthozireListKey.GlobalKey.GlobalEditKey).GetAllPublicConstantValues<string>());
            strings.AddRange(typeof(AuthozireListKey.GlobalKey.GlobalReadKey).GetAllPublicConstantValues<string>());

            return strings;
        }


    }
}
