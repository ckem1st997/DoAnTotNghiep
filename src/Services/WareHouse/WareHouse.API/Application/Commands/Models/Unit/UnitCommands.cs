namespace WareHouse.API.Application.Commands.Models
{
    public partial class UnitCommands: BaseCommands
    {
        public string UnitName { get; set; }
        public bool Inactive { get; set; }
    }
}
