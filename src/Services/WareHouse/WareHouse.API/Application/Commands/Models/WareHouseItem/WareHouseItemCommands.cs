namespace WareHouse.API.Application.Commands.Models
{
    public partial class WareHouseItemCommands: BaseCommands
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string Country { get; set; }
        public string UnitId { get; set; }
        public bool? Inactive { get; set; }
    }
}
