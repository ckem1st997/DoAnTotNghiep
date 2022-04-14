using Master.Application.Message;
using Master.Service;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Master.SignalRHubs
{
    public class ConnectRealTimeHub : Hub
    {
        private readonly IUserService _userService;

        public ConnectRealTimeHub(IUserService userService)
        {
            _userService = userService;
        }

        public async Task WareHouseBookTrachking(string id)
        {

            var res = new ResultMessageResponse()
            {
                data = id,
                success = !string.IsNullOrEmpty(id),
                message= "Dữ liệu sẽ được cập nhật sau khi có ai đó chỉnh sửa !"
            };
            await Clients.Others.SendAsync("WareHouseBookTrachkingToCLient", res, id);
        }

        public string getConnectId()
        {
            return Context.ConnectionId;
        }
    }
}