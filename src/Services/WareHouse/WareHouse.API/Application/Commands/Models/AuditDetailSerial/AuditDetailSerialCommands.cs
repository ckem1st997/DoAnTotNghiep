namespace WareHouse.API.Application.Commands.Models
{
    public partial class AuditDetailSerialCommands: BaseCommands
    {
        public string ItemId { get; set; }
        public string Serial { get; set; }
        public string AuditDetailId { get; set; }
    }
}
