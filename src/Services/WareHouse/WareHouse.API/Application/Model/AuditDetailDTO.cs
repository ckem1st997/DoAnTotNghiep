using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class AuditDetailDTO: BaseModel
    {
        public AuditDetailDTO()
        {
            AuditDetailSerials = new HashSet<AuditDetailSerialDTO>();
        }

        public string AuditId { get; set; }
        public string ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal AuditQuantity { get; set; }
        public string Conclude { get; set; }

        public virtual AuditDTO Audit { get; set; }
        public virtual WareHouseItemDTO Item { get; set; }
        public virtual ICollection<AuditDetailSerialDTO> AuditDetailSerials { get; set; }
    }
}
