    using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class WareHouseLimitDTO:BaseModel
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
        public string WareHouseName{get;set;}
        public string ItemName{get;set;}
        public virtual IEnumerable<WareHouseItemDTO> WareHouseItemDTO { get; set; }
        public virtual IEnumerable<UnitDTO> UnitDTO { get; set; }
        public virtual IEnumerable<WareHouseDTO> WareHouseDTO { get; set; }
        public virtual WareHouseItemDTO Item { get; set; }
        public virtual UnitDTO Unit { get; set; }
        public virtual WareHouseDTO WareHouse { get; set; }
    }
}
