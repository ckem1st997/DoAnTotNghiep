﻿using Master.Application.Message;
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
            //  if (_userService == null)
            //     _userService = GetServiceByInterface<IUserService>.GetService();
        }

        public async Task WareHouseBookTrachking(string id)
        {
            _userService = Context.GetHttpContext().RequestServices.GetService<IUserService>();
            var res = new ResultMessageResponse()
            {
                data = id,
                success = _userService != null,
                message = _userService != null ? "Dữ liệu được cập nhật, sau khi " + _userService.User.UserName + " chỉnh sửa !" : "Không tìm thấy dữ liệu"
            };
            await Clients.Others.SendAsync("WareHouseBookTrachkingToCLient", res, _userService.User.Id);
        }


        public async Task CreateWareHouseBookTrachking(string type)
        {

            var res = new ResultMessageResponse()
            {
                data = type,
                success = _userService != null,
                message = _userService != null ? "Dữ liệu được cập nhật, sau khi " + _userService.User.UserName + " tạo mới phiếu " + type + "!" : "Không tìm thấy dữ liệu"
            };
            await Clients.Others.SendAsync("CreateWareHouseBookTrachkingToCLient", res, _userService.User.Id);
        }

        public string getConnectId()
        {
            return Context.ConnectionId;
        }
    }
}