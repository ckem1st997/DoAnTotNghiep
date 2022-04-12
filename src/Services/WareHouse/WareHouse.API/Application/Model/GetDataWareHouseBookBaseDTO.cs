using System.Collections.Generic;

namespace WareHouse.API.Application.Model
{
    public class GetDataWareHouseBookBaseDTO
    {
        public virtual IEnumerable<WareHouseItemDTO> WareHouseItemDTO { get; set; }
        public virtual IEnumerable<UnitDTO> UnitDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetDepartmentDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetEmployeeDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetStationDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetProjectDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetCustomerDTO { get; set; }
        public virtual IEnumerable<BaseSelectDTO> GetAccountDTO { get; set; }
    }
}