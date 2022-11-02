using System;
using System.Collections.Generic;
using Share.BaseCore;

#nullable disable

namespace WareHouse.Domain.Entity
{
    public partial class OutwardDetail : BaseEntity
    {
        public OutwardDetail()
        {
            SerialWareHouses = new HashSet<SerialWareHouse>();
        }

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

        public virtual WareHouseItem Item { get; set; }
        public virtual Outward Outward { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<SerialWareHouse> SerialWareHouses { get; set; }
    }
}
