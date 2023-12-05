using Infrastructure;
using Master.Models;
using System.Threading.Tasks;

namespace Master.Service
{
    // done phân quyền với xác thực hoặc không xác thực.
    //hiện đang xoá hết cache
    //Note: 
    //- Khi phân quyền người dùng theo key, xoá cache theo userId==> xong.
    //- Khi phân quyền người người theo định danh quyền, xoá cache theo userId==> xong.
    //- Khi phân quyền định danh quyền theo key, đang xoá tất cả key,
    //nghĩ cách xoá các cache có userId được phân quyền theo định danh quyền,
    //có thể sẽ lấy danh sách userId ứng viên định danh quyền đó rồi xoá theo list userId đó==> chưa xong.
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
        public Task<UserMaster> GetUserByUserNameAsync(string userName);
        public Task<bool> CheckAuthozireByUserIdAndRoleKey(string userId, string roleKey);
        public Task CacheListRole(string userId);
        public Task CacheListRoleInactiveFalse();
        public Task<IEnumerable<string>> GetListRoleInactiveFalse();
        public Task RemoveCacheListRoleInactiveFalse();
        public Task RemoveCacheListRole(string userId);
        public Task RemoveAllCacheListRoleByUser();

        public Task<IPaginatedList<UserMaster>> GetListUserAsync(int pages, int number, string wareHouseId, string keyWords);
    }
}
