using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class Inward:BaseEntity
    {
        public Inward()
        {
            InwardDetails = new HashSet<InwardDetail>();
        }

        public string VoucherCode { get; set; }
        public DateTime VoucherDate { get; set; }
        public string WareHouseId { get; set; }
        public string Deliver { get; set; }
        public string Receiver { get; set; }
        public string VendorId { get; set; }
        public int Reason { get; set; }
        public string ReasonDescription { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual WareHouse WareHouse { get; set; }
        public virtual ICollection<InwardDetail> InwardDetails { get; set; }
    }
}
