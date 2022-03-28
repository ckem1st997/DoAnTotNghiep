using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public class ReportValueDetalsDT0 : BaseModel
    {
        public DateTime VoucherDate { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string VoucherCode { get; set; }
        public string UnitName { get; set; }
        public string WareHouseId { get; set; }
        public string Reason { get; set; }
        public decimal Beginning { get; set; }
        public decimal Import { get; set; }
        public decimal Export { get; set; }
        public decimal Balance { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
    }
}