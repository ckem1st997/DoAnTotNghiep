using GrpcGetDataToMaster;
using Share.BaseCore.Authozire;

namespace ShareImplemention
{
    public class UserInfomationService : IUserInfomationService
    {
        private readonly GrpcGetData.GrpcGetDataClient _client;

        public UserInfomationService(GrpcGetData.GrpcGetDataClient client)
        {
            _client = client;
        }

        public async Task<bool> GetAuthozireByUserIdToAuthorizeRole(string idUser, string authRole)
        {
            // get user from cache by cache - key, set cache in login
            // if cachet == null then call grpc to service master
            // get user and cache reload userrole, return user after get and check role, return result check
            // resgister service di and resgister in service
            var res = await _client.GetCreateByAsync(new Params());
            return res is not null;
        }

        public Task<bool> GetAuthozireByUserIdToKey(string iduser, string Key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetAuthozireByUserNameToKey(string username, string Key)
        {
            throw new NotImplementedException();
        }

        public void GetInfoUserByClaims()
        {
            throw new NotImplementedException();
        }
    }
}