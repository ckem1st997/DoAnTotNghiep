using GrpcGetDataToMaster;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Authentication
{
    public class UserSevice : IUserSevice
    {
        private readonly GrpcGetData.GrpcGetDataClient _client;


        public UserSevice(GrpcGetData.GrpcGetDataClient client)
        {
            _client = client;
        }

        public async Task<UserGrpc> GetUser()
        {
            var res =await _client.GetUserAsync(new Params());
            var user = new UserGrpc()
            {
                Id = res.Id,
                UserName = res.UserName,
                Create=res.Create,
                Delete=res.Delete,
                Edit=res.Edit,
                Password=res.Password,
                Read=res.Read,
                Role=res.Role,
                RoleNumber=res.RoleNumber,
                WarehouseId=res.WarehouseId,
                ListWarehouseId=res.ListWarehouseId
            };
            return user;
        }

    }
}
