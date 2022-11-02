using System;
using System.Collections.Generic;
using Share.BaseCore;

#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class WareHouseItemCategory:BaseEntity
    {
        public WareHouseItemCategory()
        {
            InverseParent = new HashSet<WareHouseItemCategory>();
            WareHouseItems = new HashSet<WareHouseItem>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public bool? Inactive { get; set; }

        public virtual WareHouseItemCategory Parent { get; set; }
        public virtual ICollection<WareHouseItemCategory> InverseParent { get; set; }
        public virtual ICollection<WareHouseItem> WareHouseItems { get; set; }
    }
}
