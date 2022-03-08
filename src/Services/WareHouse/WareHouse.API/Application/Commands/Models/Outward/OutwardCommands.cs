using System;
using System.Collections.Generic;

namespace WareHouse.API.Application.Commands.Models
{
    public partial class OutwardCommands: BaseCommands
    {
        public string VoucherCode { get; set; }
        public DateTime VoucherDate { get; set; }
        public string WareHouseId { get; set; }
        public string ToWareHouseId { get; set; }
        public string Deliver { get; set; }
        public string Receiver { get; set; }
        public int Reason { get; set; }
        public string ReasonDescription { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual IEnumerable<OutwardDetailCommands> OutwardDetails { get; set; }
    }
}
