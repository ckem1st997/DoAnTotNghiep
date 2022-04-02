using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Model
{
    public class RecallModel
    {
        public int STT { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        public decimal Quantity { get; set; }

        public string Status { get; set; }

    }
}
