using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class AuditDetail:BaseEntity
    {
        public AuditDetail()
        {
            AuditDetailSerials = new HashSet<AuditDetailSerial>();
        }

        public string AuditId { get; set; }
        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public int AuditQuantity { get; set; }
        public string Conclude { get; set; }

        public virtual Audit Audit { get; set; }
        public virtual WareHouseItem Item { get; set; }
        public virtual ICollection<AuditDetailSerial> AuditDetailSerials { get; set; }
    }
}
