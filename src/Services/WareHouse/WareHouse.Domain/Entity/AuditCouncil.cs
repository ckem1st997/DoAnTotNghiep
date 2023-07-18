using System;
using System.Collections.Generic;
using Share.BaseCore.BaseNop;
#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class AuditCouncil : BaseEntity
    {
        public string AuditId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Role { get; set; }

        public virtual Audit Audit { get; set; }
    }
}
