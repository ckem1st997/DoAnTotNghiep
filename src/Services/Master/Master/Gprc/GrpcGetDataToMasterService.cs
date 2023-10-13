using Share.Base.Core.Extensions;
using Grpc.Core;
using Infrastructure;
using Master.Extension;
using Master.Service;
using Master.SignalRHubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System;
using System.Linq;
using System.Threading.Tasks;
using Share.Base.Service.Caching.CacheName;
using Share.Base.Service.Caching;

namespace GrpcGetDataToMaster
{
    //  [Authorize]
    public class GrpcGetDataToMasterService : GrpcGetData.GrpcGetDataBase
    {

        private readonly MasterdataContext _masterdataContext;
        private readonly IUserService _userService;
        private readonly IHubContext<ConnectRealTimeHub> _hubContext;
        private readonly IHybridCachingManager _cacheExtension;



        public GrpcGetDataToMasterService(IHubContext<ConnectRealTimeHub> hubContext, IUserService userService, MasterdataContext masterdataContext, IHybridCachingManager cacheExtension)
        {
            _userService = userService;
            _masterdataContext = masterdataContext;
            _hubContext = hubContext;
            _cacheExtension = cacheExtension;
        }


        public override async Task<SaveChange> CreateHistory(HistotyModel request, ServerCallContext context)
        {
            var model = new HistoryNotication()
            {
                Id = Guid.NewGuid().ToString(),
                Body = request.Body,
                CreateDate = DateTime.Now,
                Link = request.Link,
                Method = request.Method,
                OnDelete = false,
                Read = false,
                UserName = request.UserName,
            };
            await _masterdataContext.AddAsync(model);
            var res = await _masterdataContext.SaveChangesAsync();
            if (res > 0)
            {
                var ress = new MessageResponse()
                {
                    data = request.UserName,
                    success = res > 0
                };
                await _hubContext.Clients.All.SendAsync("HistoryTrachkingToCLient", ress, request.UserName);
            }
            return new SaveChange() { Check = res > 0 };
        }

        public override async Task<SaveChange> ActiveHistory(BaseId request, ServerCallContext context)
        {
            var model = _masterdataContext.HistoryNotications.FirstOrDefault(x => x.UserName.Equals(request.Id));
            if (model == null)
                await Task.FromResult(new SaveChange() { Check = false });
            model.Read = true;
            var res = await _masterdataContext.SaveChangesAsync();
            return new SaveChange() { Check = res > 0 };
        }



        public override async Task<User> GetUser(Params request, ServerCallContext context)
        {
            var u = _userService.User;
            var res = new User()
            {
                Create = u.Create,
                Delete = u.Delete,
                Edit = u.Edit,
                Read = u.Read,
                Role = u.Role,
                UserName = u.UserName,
                WarehouseId = u.WarehouseId,
                Id = u.Id,
                RoleNumber = u.RoleNumber,
            };
            return await Task.FromResult(res);
        }

        public override async Task<ListCreateBy> GetCreateBy(Params request, ServerCallContext context)
        {
            //   var list = await _mediat.GetAllAync<ListCreateBy>("select * from FakeDataMaster where Type=1 order by Type ",null,System.Data.CommandType.Text)

            var list = new ListCreateBy();
            foreach (var item in _masterdataContext.UserMasters.ToList())
            {
                var tem = new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.UserName,
                };
                list.ListCreateBy_.Add(tem);
            }
            return await Task.FromResult(list);
        }

        public override Task<ListGetCustomer> GetCustomer(Params request, ServerCallContext context)
        {
            var list = new ListGetCustomer();
            foreach (var item in FakeData.GetCustomer())
            {
                var tem = new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                list.ListGetCustomer_.Add(tem);
            }
            return Task.FromResult(list);
        }


        public override Task<ListGetDepartment> GetDepartment(Params request, ServerCallContext context)
        {
            var list = new ListGetDepartment();
            foreach (var item in FakeData.GetDepartment())
            {
                var tem = new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                list.ListGetDepartment_.Add(tem);
            }
            return Task.FromResult(list);
        }



        public override Task<ListGetEmployee> GetEmployee(Params request, ServerCallContext context)
        {
            var list = new ListGetEmployee();
            foreach (var item in FakeData.GetEmployee())
            {
                var tem = new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                list.ListGetEmployee_.Add(tem);
            }
            return Task.FromResult(list);
        }

        public override Task<ListGetProject> GetProject(Params request, ServerCallContext context)
        {
            var list = new ListGetProject();
            foreach (var item in FakeData.GetProject())
            {
                var tem = new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                list.ListGetProject_.Add(tem);
            }
            return Task.FromResult(list);
        }
        public override Task<ListGetStation> GetStation(Params request, ServerCallContext context)
        {
            var list = new ListGetStation();
            foreach (var item in FakeData.GetStation())
            {
                var tem = new BaseSelectDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                list.ListGetStation_.Add(tem);
            }
            return Task.FromResult(list);
        }

        public override Task<User> GetUserById(BaseId request, ServerCallContext context)
        {
            return base.GetUserById(request, context);
        }

        public override async Task<SaveChange> CheckAuthozireByUserIdAndRoleKey(CheckAuthozireByUserIdAndRoleKeyModel request, ServerCallContext context)
        {
            if (request is null || string.IsNullOrEmpty(request.UserId) || string.IsNullOrEmpty(request.RoleKey))
                return new SaveChange() { Check = false };
            bool res = await _userService.CheckAuthozireByUserIdAndRoleKey(request.UserId, request.RoleKey);
            if (_cacheExtension.IsConnectedRedis)
            {
                await _userService.RemoveCacheListRole(request.UserId);
                await _userService.CacheListRole(request.UserId);
            }

            return new SaveChange() { Check = res };
        }


        [AllowAnonymous]
        public override async Task<ListRoleInactiveFalse> GetListRoleInactiveFalse(Params request, ServerCallContext context)
        {
            var res = await _userService.GetListRoleInactiveFalse();
            if (_cacheExtension.IsConnectedRedis)
            {
                await _userService.RemoveCacheListRoleInactiveFalse();
                await _userService.CacheListRoleInactiveFalse();
            }

            var result = new ListRoleInactiveFalse();
            foreach (var item in res)
            {
                result.ListRoleInactiveFalse_.Add(item);
            }
            return result;
        }
    }
}
