using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class WareHouseItemDTO : BaseModel
    {
        public WareHouseItemDTO()
        {
            AuditDetailSerials = new HashSet<AuditDetailSerialDTO>();
            AuditDetails = new HashSet<AuditDetailDTO>();
            BeginningWareHouses = new HashSet<BeginningWareHouseDTO>();
            InwardDetails = new HashSet<InwardDetailDTO>();
            OutwardDetails = new HashSet<OutwardDetailDTO>();
            SerialWareHouses = new HashSet<SerialWareHouseDTO>();
            WareHouseItemUnits = new HashSet<WareHouseItemUnitDTO>();
            WareHouseLimits = new HashSet<WareHouseLimitDTO>();
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string Country { get; set; }
        public string UnitId { get; set; }
        public bool? Inactive { get; set; }

        public virtual WareHouseItemCategoryDTO Category { get; set; }
        public virtual IEnumerable<WareHouseItemCategoryDTO> CategoryDTO { get; set; }
        public virtual IEnumerable<UnitDTO> UnitDTO { get; set; }
        public virtual IEnumerable<VendorDTO> VendorDTO { get; set; }
        public virtual UnitDTO Unit { get; set; }
        public virtual VendorDTO Vendor { get; set; }
        public virtual ICollection<AuditDetailSerialDTO> AuditDetailSerials { get; set; }
        public virtual ICollection<AuditDetailDTO> AuditDetails { get; set; }
        public virtual ICollection<BeginningWareHouseDTO> BeginningWareHouses { get; set; }
        public virtual ICollection<InwardDetailDTO> InwardDetails { get; set; }
        public virtual ICollection<OutwardDetailDTO> OutwardDetails { get; set; }
        public virtual ICollection<SerialWareHouseDTO> SerialWareHouses { get; set; }
        public virtual ICollection<WareHouseItemUnitDTO> WareHouseItemUnits { get; set; }
        public virtual ICollection<WareHouseLimitDTO> WareHouseLimits { get; set; }
    }
}
