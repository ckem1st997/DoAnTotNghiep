using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class SelectTopBengingDTO: BaseModel
    {
        public string WareHouseId { get; set; }
        public string Name { get; set; }
        public string WareHouseItemCode { get; set; }
        public string WareHouseItemName { get; set; }
        public string Balance { get; set; }
         public string UnitName { get; set; }
    }
}
