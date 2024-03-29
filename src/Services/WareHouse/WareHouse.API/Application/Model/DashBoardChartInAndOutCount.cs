﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Model
{
    public class DashBoardChartInAndOutCount
    {
        public IEnumerable<BaseCountChartByMouthOrYear> Inward { get; set; }
        public IEnumerable<BaseCountChartByMouthOrYear> Outward { get; set; }
    }
}
