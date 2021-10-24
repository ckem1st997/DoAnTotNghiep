using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class WarehouseBalance:BaseEntity
    {
        public string ItemId { get; set; }
        public string WarehouseId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}
