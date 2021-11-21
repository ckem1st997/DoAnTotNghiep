using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class AuditDetailSerialDTO: BaseModel
    {
        public string ItemId { get; set; }
        public string Serial { get; set; }
        public string AuditDetailId { get; set; }

        public virtual AuditDetailDTO AuditDetail { get; set; }
        public virtual WareHouseItemDTO Item { get; set; }
    }
}
