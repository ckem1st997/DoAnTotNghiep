using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class WareHouseItemCategoryDTO: BaseModel
    {
        public WareHouseItemCategoryDTO()
        {
            InverseParent = new HashSet<WareHouseItemCategoryDTO>();
            WareHouseItems = new HashSet<WareHouseItemDTO>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public bool? Inactive { get; set; }

        public virtual WareHouseItemCategoryDTO Parent { get; set; }
        public virtual ICollection<WareHouseItemCategoryDTO> InverseParent { get; set; }
        public virtual ICollection<WareHouseItemDTO> WareHouseItems { get; set; }
    }
}
