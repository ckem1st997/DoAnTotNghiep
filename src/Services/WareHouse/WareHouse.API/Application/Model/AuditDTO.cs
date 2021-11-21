using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class AuditDTO : BaseModel
    {
        public AuditDTO()
        {
            AuditCouncils = new HashSet<AuditCouncilDTO>();
            AuditDetails = new HashSet<AuditDetailDTO>();
        }

        public string VoucherCode { get; set; }
        public DateTime VoucherDate { get; set; }
        public string WareHouseId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual WareHouseDTO WareHouse { get; set; }
        public virtual ICollection<AuditCouncilDTO> AuditCouncils { get; set; }
        public virtual ICollection<AuditDetailDTO> AuditDetails { get; set; }
    }
}
