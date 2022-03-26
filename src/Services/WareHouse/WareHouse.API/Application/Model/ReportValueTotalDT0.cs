using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public class ReportValueTotalDT0 : BaseModel
    {
        public decimal Balance { get; set; }
        public decimal Export { get; set; }
        public decimal Import { get; set; }
        public decimal Beginning { get; set; }
        public string UnitName { get; set; }
        public string WareHouseItemName { get; set; }
        public string WareHouseItemCode { get; set; }

    }
}