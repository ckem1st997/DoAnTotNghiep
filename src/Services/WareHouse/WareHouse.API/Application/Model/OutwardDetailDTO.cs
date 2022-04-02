using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class OutwardDetailDTO : BaseModel
    {
        public OutwardDetailDTO()
        {
            SerialWareHouses = new HashSet<SerialWareHouseDTO>();
        }

        public string OutwardId { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string Code { get; set; }
        public string UnitName { get; set; }

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

        public virtual WareHouseItemDTO Item { get; set; }
        public virtual OutwardDTO Outward { get; set; }
        public virtual UnitDTO Unit { get; set; }
        public virtual ICollection<SerialWareHouseDTO> SerialWareHouses { get; set; }

        public virtual IEnumerable<WareHouseItemDTO> WareHouseItemDTO { get; set; }
        public virtual IEnumerable<UnitDTO> UnitDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetDepartmentDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetEmployeeDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetStationDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetProjectDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetCustomerDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetAccountDTO { get; set; }
        public virtual InwardDTO Inward { get; set; }
    }
}
