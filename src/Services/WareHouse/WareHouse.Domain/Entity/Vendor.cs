using System;
using System.Collections.Generic;


#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class Vendor:BaseEntity
    {
        public Vendor()
        {
            Inwards = new HashSet<Inward>();
            WareHouseItems = new HashSet<WareHouseItem>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public bool Inactive { get; set; }

        public virtual ICollection<Inward> Inwards { get; set; }
        public virtual ICollection<WareHouseItem> WareHouseItems { get; set; }
    }
}
