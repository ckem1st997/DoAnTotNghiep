using System;
using System.Collections.Generic;

#nullable disable

namespace WareHouse.API.Application.Model
{
    public partial class SelectTopWareHouseDTO: BaseModel
    {
        public string WareHouseId { get; set; }
        public string Name { get; set; }
        public string SumBalance { get; set; }
    }
}
