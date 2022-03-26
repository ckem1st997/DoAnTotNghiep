using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class DashBoardSelectTopInAndOut : BaseModel
    {
        public int Count { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string UnitName { get; set; }
        public decimal SumQuantity { get; set; }
        public decimal SumPrice { get; set; }
    }
}
