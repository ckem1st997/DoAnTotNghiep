using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class SerialWareHouseDTO: BaseModel
    {
        public string ItemId { get; set; }
        public string Serial { get; set; }
        public string InwardDetailId { get; set; }
        public string OutwardDetailId { get; set; }
        public bool IsOver { get; set; }

        public virtual InwardDetailDTO InwardDetail { get; set; }
        public virtual WareHouseItemDTO Item { get; set; }
        public virtual OutwardDetailDTO OutwardDetail { get; set; }
    }
}
