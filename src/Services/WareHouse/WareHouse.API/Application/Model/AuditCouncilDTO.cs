using Share.Base.Core.Infrastructure;
using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class AuditCouncilDTO : BaseModel
    {
        public string AuditId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Role { get; set; }

        public virtual AuditDTO Audit { get; set; }
    }
}
