using System;
using System.Collections.Generic;
using Share.BaseCore;

#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class SerialWareHouse:BaseEntity
    {
        public string ItemId { get; set; }
        public string Serial { get; set; }
        public string InwardDetailId { get; set; }
        public string OutwardDetailId { get; set; }
        public bool IsOver { get; set; }

        public virtual InwardDetail InwardDetail { get; set; }
        public virtual WareHouseItem Item { get; set; }
        public virtual OutwardDetail OutwardDetail { get; set; }
    }
}
