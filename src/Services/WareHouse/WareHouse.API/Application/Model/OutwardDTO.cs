﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class OutwardDTO : BaseModel
    {
        public OutwardDTO()
        {
            OutwardDetails = new HashSet<OutwardDetailDTO>();
        }
        public string Voucher { get; set; }
        public string VoucherCode { get; set; }
        public DateTime VoucherDate { get; set; } = DateTime.Now;
        public string WareHouseId { get; set; }
        public string ToWareHouseId { get; set; }
        public string Deliver { get; set; }
        public string Receiver { get; set; }
        public string Reason { get; set; }
        public string ReasonDescription { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string DeliverPhone { get; set; }

        public string DeliverAddress { get; set; }

        public string DeliverDepartment { get; set; }

        public string ReceiverPhone { get; set; }

        public string ReceiverAddress { get; set; }

        public string ReceiverDepartment { get; set; }

        public virtual WareHouseDTO ToWareHouse { get; set; }
        public virtual WareHouseDTO WareHouse { get; set; }
        public virtual ICollection<OutwardDetailDTO> OutwardDetails { get; set; }
        public virtual IEnumerable<WareHouseDTO> WareHouseDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetCreateBy { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetModifiedBy { get; set; }
    }
}
