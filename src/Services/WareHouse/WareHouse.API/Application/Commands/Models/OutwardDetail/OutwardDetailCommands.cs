using System.Collections.Generic;

namespace WareHouse.API.Application.Commands.Models
{
    public partial class OutwardDetailCommands : BaseCommands
    {
        public string OutwardId { get; set; }
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public decimal Uiquantity { get; set; }
        public decimal Uiprice { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string StationId { get; set; }
        public string StationName { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string AccountMore { get; set; }

        public string AccountYes { get; set; }

        public string Status { get; set; }

        public virtual IEnumerable<SerialWareHouseCommands> SerialWareHouses { get; set; }
    }
}
