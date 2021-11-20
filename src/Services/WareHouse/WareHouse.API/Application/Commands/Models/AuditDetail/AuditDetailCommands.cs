namespace WareHouse.API.Application.Commands.Models
{
    public partial class AuditDetailCommands: BaseCommands
    {
        public string AuditId { get; set; }
        public string ItemId { get; set; }
        public decimal Quantity { get; set; }
        public decimal AuditQuantity { get; set; }
        public string Conclude { get; set; }
    }
}
