using Infrastructure;
using Master.Models;
using System.Threading.Tasks;

namespace Master.Service
{
    public interface IUserService
    {

        public string GenerateJWT(LoginModel model);
        public Task<bool> Register(RegisterModel model);
        /// <summary>
        /// true nếu không tồn tại
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool CheckUser(string name);
        public UserMaster GetUserById(string id);
    }
}
