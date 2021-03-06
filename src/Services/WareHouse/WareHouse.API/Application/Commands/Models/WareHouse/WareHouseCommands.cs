using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Commands.Models
{
    public partial class WareHouseCommands : BaseCommands
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ParentId { get; set; }
        public string Path { get; set; }
        public bool Inactive { get; set; } = false;
    }
}
