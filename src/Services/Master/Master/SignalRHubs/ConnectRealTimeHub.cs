using Master.Application.Message;
using Master.Extension;
using Master.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Master.SignalRHubs
{
    [Authorize]
    public class ConnectRealTimeHub : Hub
    {
        private IUserService _userService;

        public ConnectRealTimeHub()
        {

        }

        private void GetUserByService()
        {
            if (_userService == null)
                _userService = Context.GetHttpContext().RequestServices.GetService<IUserService>();
        }

        public async Task WareHouseBookTrachking(string id)
        {
            GetUserByService();
            var res = new ResultMessageResponse()
            {
                data = id,
                success = _userService != null,
                message = _userService != null ? "Dữ liệu cập nhật, sau khi " + _userService.User.UserName + " chỉnh sửa !" : "Không tìm thấy dữ liệu"
            };
            await Clients.Others.SendAsync("WareHouseBookTrachkingToCLient", res, _userService.User.Id);
        }


        public async Task CreateWareHouseBookTrachking(string type)
        {
            GetUserByService();
            var res = new ResultMessageResponse()
            {
                data = type,
                success = _userService != null,
                message = _userService != null ? "Dữ liệu cập nhật, sau khi " + _userService.User.UserName + " tạo mới phiếu " + type + "!" : "Không tìm thấy dữ liệu"
            };
            await Clients.Others.SendAsync("CreateWareHouseBookTrachkingToCLient", res, _userService.User.Id);
        }


        public async Task DeleteWareHouseBookTrachking(string type,string id)
        {
            GetUserByService();
            var res = new ResultMessageResponse()
            {
                data = id,
                success = _userService != null,
                message = _userService != null ? "Dữ liệu cập nhật, sau khi " + _userService.User.UserName + " xoá phiếu " + type + "!" : "Không tìm thấy dữ liệu"
            };
            await Clients.Others.SendAsync("DeleteWareHouseBookTrachkingToCLient", res, _userService.User.Id);
        }

        public string getConnectId()
        {
            return Context.ConnectionId;
        }
    }
}