using System;
using System.Collections.Generic;
using Share.BaseCore;
#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class WareHouseLimit : BaseEntity
    {
        public string WareHouseId { get; set; }
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual WareHouseItem Item { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual WareHouse WareHouse { get; set; }
    }
}
