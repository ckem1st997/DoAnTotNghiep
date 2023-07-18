using System;
using System.Collections.Generic;
using Share.BaseCore.BaseNop;

#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class WareHouseItemUnit:BaseEntity
    {
        public string ItemId { get; set; }
        public string UnitId { get; set; }
      //  public string UnitName { get; set; }
        public int ConvertRate { get; set; }
        public bool? IsPrimary { get; set; }

        public virtual WareHouseItem Item { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
