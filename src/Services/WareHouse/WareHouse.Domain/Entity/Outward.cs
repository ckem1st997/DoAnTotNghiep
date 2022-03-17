using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class Outward:BaseEntity
    {
        public Outward()
        {
            OutwardDetails = new HashSet<OutwardDetail>();
        }
        public string Voucher { get; set; }
        public string VoucherCode { get; set; }
        public DateTime VoucherDate { get; set; }
        public string WareHouseId { get; set; }
        public string ToWareHouseId { get; set; }
        public string Deliver { get; set; }
        public string Receiver { get; set; }
        public string Reason { get; set; }
        public string ReasonDescription { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string DeliverPhone { get; set; }

        public string DeliverAddress { get; set; }

        public string DeliverDepartment { get; set; }

        public string ReceiverPhone { get; set; }

        public string ReceiverAddress { get; set; }

        public string ReceiverDepartment { get; set; }
        public virtual WareHouse ToWareHouse { get; set; }
        public virtual WareHouse WareHouse { get; set; }
        public virtual ICollection<OutwardDetail> OutwardDetails { get; set; }
    }
}
