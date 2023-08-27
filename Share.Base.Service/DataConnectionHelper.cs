using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service
{
    public static class DataConnectionHelper
    {
        public static class ConnectionStringNames
        {
            public const string Master = "Master";
            public const string Warehouse = "Warehouse";
            public const string Asset = "Asset";
            public const string Ticket = "Ticket";
            public const string Dashboard = "Dashboard";
            public const string File = "File";
            public const string Notify = "Notify";
        }

        public static class ConnectionString
        {
            public const string Master = "MasterDataContext";
            public const string Warehouse = "WarehouseManagementContext";
            public const string Asset = "Asset";
            public const string Ticket = "Ticket";
            public const string Dashboard = "Dashboard";
            public const string File = "File";
            public const string Notify = "Notify";
        }
        // name biến truyền qua hàm khơir tạo
        public const string ParameterName = "context";
    }
}
