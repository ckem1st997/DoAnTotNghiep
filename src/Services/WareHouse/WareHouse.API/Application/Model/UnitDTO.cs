using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class UnitDTO: BaseModel
    {
        public UnitDTO()
        {
            BeginningWareHouses = new HashSet<BeginningWareHouseDTO>();
            InwardDetails = new HashSet<InwardDetailDTO>();
            OutwardDetails = new HashSet<OutwardDetailDTO>();
            WareHouseItemUnits = new HashSet<WareHouseItemUnitDTO>();
            WareHouseItems = new HashSet<WareHouseItemDTO>();
            WareHouseLimits = new HashSet<WareHouseLimitDTO>();
        }

        public string UnitName { get; set; }
        public bool Inactive { get; set; }

        public virtual ICollection<BeginningWareHouseDTO> BeginningWareHouses { get; set; }
        public virtual ICollection<InwardDetailDTO> InwardDetails { get; set; }
        public virtual ICollection<OutwardDetailDTO> OutwardDetails { get; set; }
        public virtual ICollection<WareHouseItemUnitDTO> WareHouseItemUnits { get; set; }
        public virtual ICollection<WareHouseItemDTO> WareHouseItems { get; set; }
        public virtual ICollection<WareHouseLimitDTO> WareHouseLimits { get; set; }
    }
}
