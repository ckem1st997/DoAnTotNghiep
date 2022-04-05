using Master.Models;
using System.Threading.Tasks;

namespace Master.Service
{
    public interface IUserService
    {

        public string GenerateJWT(LoginModel model);
        public Task<bool> Register(RegisterModel model);
    }
}
