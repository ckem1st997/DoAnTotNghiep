using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.Domain.Entity
{
    /// trigger
     public partial class WareHouseItem:BaseEntity
    {
        public WareHouseItem()
        {
            AuditDetailSerials = new HashSet<AuditDetailSerial>();
            AuditDetails = new HashSet<AuditDetail>();
            BeginningWareHouses = new HashSet<BeginningWareHouse>();
            InwardDetails = new HashSet<InwardDetail>();
            OutwardDetails = new HashSet<OutwardDetail>();
            SerialWareHouses = new HashSet<SerialWareHouse>();
            WareHouseItemUnits = new HashSet<WareHouseItemUnit>();
            WareHouseLimits = new HashSet<WareHouseLimit>();
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

        public virtual WareHouseItemCategory Category { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<AuditDetailSerial> AuditDetailSerials { get; set; }
        public virtual ICollection<AuditDetail> AuditDetails { get; set; }
        public virtual ICollection<BeginningWareHouse> BeginningWareHouses { get; set; }
        public virtual ICollection<InwardDetail> InwardDetails { get; set; }
        public virtual ICollection<OutwardDetail> OutwardDetails { get; set; }
        public virtual ICollection<SerialWareHouse> SerialWareHouses { get; set; }
        public virtual ICollection<WareHouseItemUnit> WareHouseItemUnits { get; set; }
        public virtual ICollection<WareHouseLimit> WareHouseLimits { get; set; }
    }
}