namespace WareHouse.API.Application.Commands.Models
{
    public partial class WarehouseBalanceCommands: BaseCommands
    {
        public string ItemId { get; set; }
        public string WarehouseId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}
