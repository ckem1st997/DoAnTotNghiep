using Base.Events;
using GrpcGetDataToMaster;
//using GrpcGetDataToMaster;
using Serilog;
using System;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Authentication
{
    // thử xoá connect service grpc, rồi import sharegrpcfull và chạy thử
    // nếu được thì sẽ có một nơi chứa cả file server và client
    // các service muốn dùng dạng nào thì có thể import vô mà cấu hình
    public class UserSevice : IUserSevice
    {
        private readonly GrpcGetData.GrpcGetDataClient _client;


        public UserSevice(GrpcGetData.GrpcGetDataClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<bool> ActiveHistory(string UserName)
        {
            var res = await _client.ActiveHistoryAsync(new BaseId() { Id = UserName });
            return res.Check;
        }

        public async Task<bool> CheckWareHouseIdByUser(string idWareHouse)
        {
            var user = await _client.GetUserAsync(new Params());
            if (user.RoleNumber == 3)
                return true;
            return user.WarehouseId.Contains(idWareHouse);
        }

        public async Task<bool> CreateHistory(CreateHistoryIntegrationEvent create)
        {
            var model = new HistotyModel()
            {
                Body = create.Body,
                UserName = create.UserName,
                Method = create.Method,
                Link = create.Link,
                Read = create.Read
            };
            var res = await _client.CreateHistoryAsync(model);
            Log.Information("Creating KafKa Topic by Service to publish event: {EventId} ({EventName})", create.Id, nameof(CreateHistoryIntegrationEvent));
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
