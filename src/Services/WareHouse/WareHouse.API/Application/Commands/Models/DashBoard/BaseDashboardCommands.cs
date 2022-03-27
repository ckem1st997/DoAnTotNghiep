using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WareHouse.API.Application.Extensions;

namespace WareHouse.API.Application.Commands.Models
{

    public partial class BaseDashboardCommands
    {
        public DateTime? searchByDay { get; set; }
        public int searchByMounth { get; set; }
        public int searchByYear { get; set; }
        public string order { get; set; }

        public SelectTopWareHouseBook selectTopWareHouseBook { get; set; }
    }
}
