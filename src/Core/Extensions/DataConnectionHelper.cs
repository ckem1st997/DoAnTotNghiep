using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
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

        public const string ParameterName = "dataConnection";
    }
}
