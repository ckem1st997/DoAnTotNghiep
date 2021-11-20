namespace WareHouse.API.Application.Commands.Models
{
    public partial class AuditCouncilCommands: BaseCommands
    {
        public string AuditId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Role { get; set; }
    }
}
