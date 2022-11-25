using Infrastructure;
using Master.Models;
using System.Threading.Tasks;

namespace Master.Service
{
    public interface IUserService
    {
        public UserMaster User { get; }
        public Task<bool> Register(RegisterModel model);
        public Task<bool> Login(LoginModel model);
        /// <summary>
        /// true nếu không tồn tại
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CheckUser(string name);
        public Task<bool> UpdateUser(UserMaster model);
        public Task<bool> SetRoleToUser(UserMaster model);
        public Task<bool> CheckActiveUser(string userName);
        public UserMaster GetUserById(string id);
        public UserMaster GetUserByUserName(string userName);
        public Task<bool> CheckAuthozireByUserIdAndRoleKey(string userId, string roleKey);
        public Task CacheListRole(string userId);
        public Task RemoveCacheListRole(string userId);
        public Task<IPaginatedList<UserMaster>> GetListUserAsync(int pages, int number, string wareHouseId, string keyWords);
    }
}
