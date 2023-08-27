using Elastic.Apm.Api;
using GrpcGetDataToMaster;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Serilog;
using Share.Base.Service.Caching.CacheName;
using Share.Base.Service.Security;
using ShareModels.Models;
using System.Text;
using System.Threading;

namespace ShareImplemention
{
    public class UserInfomationService : IUserInfomationService
    {
        private readonly GrpcGetData.GrpcGetDataClient _client;
        private readonly IDistributedCache _cache;
        private readonly ICacheExtension _cacheExtension;

        public UserInfomationService(GrpcGetData.GrpcGetDataClient client, IDistributedCache cache, ICacheExtension cacheExtension)
        {
            _client = client;
            _cache = cache;
            _cacheExtension = cacheExtension;
        }

        public async Task<bool> GetAuthozireByUserId(string idUser, string authRole)
        {


            // viết thêm phần check key role
            // cache list role
            // mỗi lần vô đây thì bước đầu là lấy list role từ cache, nếu không lấy list từ grpc hoặc check bằng grpc
            // xem active có true không, nếu true thì tiếp tục check, nếu false thì tức là key này không hoạt động
            // action dùng key này sẽ không cần xác thực mà cho phép truy cập luôn, hàm này trả về true luôn
            // nếu là true tức là cần xác thực, check xem request có xác thực chưa, nếu chưa thì return 401

            // list active false
            // var listRoleCache = await _cache.GetAsync(string.Format(ListRoleCacheName.UserListRoleCache, false));
            var listRoleFalse = new List<string>();
            var listRoleCache = _cacheExtension.IsConnected? await _cache.GetAsync(string.Format(ListRoleCacheName.UserListRoleCache, false)):default;
            if (listRoleCache is null)
            {
                var listRoleGrpc = await _client.GetListRoleInactiveFalseAsync(new Params());
                listRoleFalse = listRoleGrpc.ListRoleInactiveFalse_.ToList();
            }
            else
            {
                listRoleFalse = JsonConvert.DeserializeObject<List<string>>(Encoding.UTF8.GetString(listRoleCache));

            }
            if (listRoleFalse is not null && listRoleFalse.Any())
            {
                // listRoleCache là null thì get by grpc
                var getRole = listRoleFalse.FirstOrDefault(x => x.Equals(authRole));
                // key không hoạt động => trả về luôn
                if (getRole != null)
                    return true;
                // tới đây tức là key active mà userid truyền vào là chưa xác thực => return false
                if (idUser.Equals("IsAuthenticatedFalse"))
                    return false;
            }


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

            // get list role by user
            var cachedResponse = _cacheExtension.IsConnected ? await _cache.GetAsync(string.Format(UserListRoleCacheName.UserListRoleCache, idUser)):default;

            // trước là check, có thì true, không thì check bằng grpc
            // giờ chech luôn
            if (cachedResponse is not null)
            {
                var resList = JsonConvert.DeserializeObject<List<string>>(Encoding.UTF8.GetString(cachedResponse));
                //if (resList is not null && resList.Contains(authRole))
                return resList is not null && resList.Contains(authRole);
            }


            // cache null call grpc and reload cache
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