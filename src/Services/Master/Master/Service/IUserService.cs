using Infrastructure;
using Master.Models;
using System.Threading.Tasks;

namespace Master.Service
{
    public interface IUserService
    {
        public UserMaster User { get; }
        public string GenerateJWT(LoginModel model);
        public Task<bool> Register(RegisterModel model);
        /// <summary>
        /// true nếu không tồn tại
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CheckUser(string name);
        public Task<bool> UpdateUser(UserMaster model);
        public UserMaster GetUserById(string id);
    }
}
