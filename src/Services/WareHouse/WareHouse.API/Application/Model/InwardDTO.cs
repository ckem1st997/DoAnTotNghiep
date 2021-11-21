using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class InwardDTO: BaseModel
    {
        public InwardDTO()
        {
            InwardDetails = new HashSet<InwardDetailDTO>();
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

        public virtual VendorDTO Vendor { get; set; }
        public virtual WareHouseDTO WareHouse { get; set; }
        public virtual ICollection<InwardDetailDTO> InwardDetails { get; set; }
    }
}
