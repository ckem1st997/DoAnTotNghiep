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

        public async Task<bool> ActiveHistory(string UserName)
        {
          var res=await _client.ActiveHistoryAsync(new BaseId() { Id = UserName });
            return res.Check;
        }

        public async Task<bool> CheckWareHouseIdByUser(string idWareHouse)
        {
            var user = await _client.GetUserAsync(new Params());
            if (user.RoleNumber == 3)
                return true;
            return user.WarehouseId.Contains(idWareHouse);
        }

        public async Task<bool> CreateHistory(string UserName, string Method, string Body, bool Read, string Link)
        {
            var model = new HistotyModel()
            {
                Body = Body,
                UserName = UserName,
                Method = Method,
                Link = Link,
                Read = Read
            };
            var res = await _client.CreateHistoryAsync(model);
            return res.Check;
        }

        public async Task<UserGrpc> GetUser()
        {
            var res = await _client.GetUserAsync(new Params());
            var user = new UserGrpc()
            {
                Id = res.Id,
                UserName = res.UserName,
                Create = res.Create,
                Delete = res.Delete,
                Edit = res.Edit,
                Password = res.Password,
                Read = res.Read,
                Role = res.Role,
                RoleNumber = res.RoleNumber,
                WarehouseId = res.WarehouseId,
                ListWarehouseId = res.ListWarehouseId
            };
            return user;
        }

    }
}
