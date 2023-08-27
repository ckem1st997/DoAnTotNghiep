
using Share.Base.Core.Infrastructure;
using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class Audit :BaseEntity
    {
        public Audit()
        {
            AuditCouncils = new HashSet<AuditCouncil>();
            AuditDetails = new HashSet<AuditDetail>();
        }

        public string VoucherCode { get; set; }
        public DateTime VoucherDate { get; set; }
        public string WareHouseId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public virtual WareHouse WareHouse { get; set; }
        public virtual ICollection<AuditCouncil> AuditCouncils { get; set; }
        public virtual ICollection<AuditDetail> AuditDetails { get; set; }
    }
}
