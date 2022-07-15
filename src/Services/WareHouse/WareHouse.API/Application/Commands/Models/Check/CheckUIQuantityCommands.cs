using System;

namespace WareHouse.API.Application.Commands.Models
{
    public partial class CheckUIQuantityCommands
    {
        public string WareHouseId { get; set; }
        public string ItemId { get; set; }
        public string UnitId { get; set; }
    }
}
