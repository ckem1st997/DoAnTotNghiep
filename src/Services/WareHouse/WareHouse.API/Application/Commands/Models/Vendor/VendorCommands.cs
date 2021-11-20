namespace WareHouse.API.Application.Commands.Models
{
    public partial class VendorCommands: BaseCommands
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public bool Inactive { get; set; }
    }
}
