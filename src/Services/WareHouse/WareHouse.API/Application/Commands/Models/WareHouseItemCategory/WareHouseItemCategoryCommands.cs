namespace WareHouse.API.Application.Commands.Models
{
    public partial class WareHouseItemCategoryCommands: BaseCommands
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public bool? Inactive { get; set; }

    }
}
