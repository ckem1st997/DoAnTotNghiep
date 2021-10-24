using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class AuditDetailSerial:BaseEntity
    {
        public string ItemId { get; set; }
        public string Serial { get; set; }
        public string AuditDetailId { get; set; }

        public virtual AuditDetail AuditDetail { get; set; }
        public virtual WareHouseItem Item { get; set; }
    }
}
