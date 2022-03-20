using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public class ReportValueModel : BaseModel
    {
        public DateTime VoucherDate { get; set; }

        public string VoucherCodeExport { get; set; }
        public string VoucherCodeImport { get; set; }
        public string UserId { get; set; }
        public string ProjectId { get; set; }
        public string DepartmentId { get; set; }
        public string NoteRender { get; set; }
        public string ProjectName { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public string Purpose { get; set; }
        public string Category { get; set; }
        public dynamic UIBalance { get; set; }
        public dynamic UIExport { get; set; }
        public dynamic UIImport { get; set; }
        public decimal Balance { get; set; }
        public decimal Export { get; set; }
        public decimal Import { get; set; }
        public decimal Beginning { get; set; }
        public string Serial { get; set; }
        public string UnitName { get; set; }
        public string WareHouseItemName { get; set; }
        public string WareHouseItemCode { get; set; }
        public string WareHouseItemId { get; set; }
        public string WareHouseId { get; set; }
        public DateTime Moment { get; set; }
        // public IList<NoteReportModel> Note { get; set; }
        // public IList<VoucherCodeReportModel> VoucherCode { get; set; }

    }
}