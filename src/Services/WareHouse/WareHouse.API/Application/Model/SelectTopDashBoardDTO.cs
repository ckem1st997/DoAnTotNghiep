using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Model
{
    public class SelectTopDashBoardDTO
    {
        public DashBoardSelectTopInAndOut ItemCountMax { get; set; }
        public DashBoardSelectTopInAndOut ItemCountMin { get; set; }
        public SelectTopWareHouseDTO WareHouseBeginningCountMax { get; set; }
        public SelectTopWareHouseDTO WareHouseBeginningCountMin { get; set; }
    }
}
