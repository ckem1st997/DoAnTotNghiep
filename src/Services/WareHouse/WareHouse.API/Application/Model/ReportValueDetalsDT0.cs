using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public class ReportValueDetalsDT0 : BaseModel
    {
        public DateTime VoucherDate { get; set; }
        public string VoucherCode { get; set; }
        public string Code { get; set; }

        public string ProjectName { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public decimal Balance { get; set; }
        public decimal Export { get; set; }
        public decimal Import { get; set; }
        public decimal Beginning { get; set; }
        public string UnitName { get; set; }
        public string WareHouseItemName { get; set; }
        public string WareHouseItemCode { get; set; }
        // public IList<NoteReportModel> Note { get; set; }
        // public IList<VoucherCodeReportModel> VoucherCode { get; set; }

    }
}