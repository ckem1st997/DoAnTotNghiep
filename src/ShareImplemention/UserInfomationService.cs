using Elastic.Apm.Api;
using GrpcGetDataToMaster;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Nest;
using Newtonsoft.Json;
using Serilog;
using Share.BaseCore.Authozire;
using Share.BaseCore.Cache.CacheName;
using ShareModels.Models;
using System.Text;
using System.Threading;

namespace ShareImplemention
{
    public class UserInfomationService : IUserInfomationService
    {
        private readonly GrpcGetData.GrpcGetDataClient _client;
        private readonly IDistributedCache _cache;

        public UserInfomationService(GrpcGetData.GrpcGetDataClient client, IDistributedCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public async Task<bool> GetAuthozireByUserId(string idUser, string authRole)
        {
            // get user from cache by cache - key, set cache in login
            // if cachet == null then call grpc to service master
            // get user and cache reload userrole, return user after get and check role, return result check
            // resgister service di and resgister in service
            // easy cache
            //var cachedResponse = JsonConvert.DeserializeObject<UserListRoleModel>(Encoding.UTF8.GetString(await _cache.GetAsync(idUser)));
            //if (cachedResponse is not null)
            //    return cachedResponse.ListKey.Contains(authRole);
            if (string.IsNullOrEmpty(idUser))
                return false;
            var cachedResponse = await _cache.GetAsync(string.Format(UserListRoleCacheName.UserListRoleCache, idUser));

            if (cachedResponse is not null)
            {
                var resList = JsonConvert.DeserializeObject<List<string>>(Encoding.UTF8.GetString(cachedResponse));
                if (resList is not null && resList.Contains(authRole))
                    return true;
            }

            // viết thêm phần check key role
            // cache list role
            // mỗi lần vô đây thì bước đầu là lấy list role từ cache, nếu không lấy list từ grpc hoặc check bằng grpc
            // xem active có true không, nếu true thì tiếp tục check, nếu false thì tức là key này không hoạt động
            // action dùng key này sẽ không cần xác thực mà cho phép truy cập luôn, hàm này trả về true luôn
            // nếu là true tức là cần xác thực, check xem request có xác thực chưa, nếu chưa thì return 401

            var listRoleCache = new List<string>();
            // listRoleCache là null thì get by grpc
            var getRole = listRoleCache.FirstOrDefault(x => x.Equals(authRole));
            // không có trong list
            if (getRole is null)
                return false;
            // key không hoạt động và không có ai xác thực
            if (getRole.Equals("active false") && idUser.Trim().Equals("IsAuthenticatedFalse"))
                return true;


            SaveChange res = await _client.CheckAuthozireByUserIdAndRoleKeyAsync(new CheckAuthozireByUserIdAndRoleKeyModel()
            {
                RoleKey = authRole,
                UserId = idUser
            });
            return res is not null && res.Check;
        }

        public void GetInfoUserByClaims()
        {
            throw new NotImplementedException();
        }
    }
}