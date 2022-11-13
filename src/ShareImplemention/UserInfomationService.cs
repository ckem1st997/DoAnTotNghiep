using GrpcGetDataToMaster;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Share.BaseCore.Authozire;
using ShareModels.Models;
using System.Text;

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