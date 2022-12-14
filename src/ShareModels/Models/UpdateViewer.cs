using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModels.Models
{
    public class UpdateViewer
    {
        public string Id { get; set; }
        public WareHouseBookEnum TypeWareHouse { get; set; }
        public int Viewer { get; set; }
    }

    public enum WareHouseBookEnum
    {
        Inward,
        Outward
    }
}
