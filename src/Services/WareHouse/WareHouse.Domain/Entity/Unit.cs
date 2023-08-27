using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class Unit:BaseEntity
    {
        public Unit()
        {
            BeginningWareHouses = new HashSet<BeginningWareHouse>();
            InwardDetails = new HashSet<InwardDetail>();
            OutwardDetails = new HashSet<OutwardDetail>();
            WareHouseItemUnits = new HashSet<WareHouseItemUnit>();
            WareHouseItems = new HashSet<WareHouseItem>();
            WareHouseLimits = new HashSet<WareHouseLimit>();
        }

        public string UnitName { get; set; }
        public bool Inactive { get; set; }

        public virtual ICollection<BeginningWareHouse> BeginningWareHouses { get; set; }
        public virtual ICollection<InwardDetail> InwardDetails { get; set; }
        public virtual ICollection<OutwardDetail> OutwardDetails { get; set; }
        public virtual ICollection<WareHouseItemUnit> WareHouseItemUnits { get; set; }
        public virtual ICollection<WareHouseItem> WareHouseItems { get; set; }
        public virtual ICollection<WareHouseLimit> WareHouseLimits { get; set; }
    }
}
