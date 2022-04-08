namespace WareHouse.API.Application.Model
{
    public class UserGrpc:BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? InActive { get; set; }
        public string Role { get; set; }
        public int RoleNumber { get; set; }
        public bool Read { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public string WarehouseId { get; set; }
    }
}
