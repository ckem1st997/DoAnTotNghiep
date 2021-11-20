namespace WareHouse.API.Application.Commands.Models
{
    public partial class WareHouseItemUnitCommands: BaseCommands
    {
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public int ConvertRate { get; set; }
        public bool? IsPrimary { get; set; }
    }
}
