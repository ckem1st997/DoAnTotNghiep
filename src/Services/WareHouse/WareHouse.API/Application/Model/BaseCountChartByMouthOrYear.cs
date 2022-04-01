using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Model
{
    public class BaseCountChartByMouthOrYear
    {
        public string Name { get; set; }
        public string NameParent { get; set; }
        public int Count { get; set; }
    }
}
