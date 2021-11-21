using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class WareHouseItemUnitDTO: BaseModel
    {
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public int ConvertRate { get; set; }
        public bool? IsPrimary { get; set; }

        public virtual WareHouseItemDTO Item { get; set; }
        public virtual UnitDTO Unit { get; set; }
    }
}
