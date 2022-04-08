using System.Threading.Tasks;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Authentication
{
    public interface IUserSevice
    {
        public Task<UserGrpc> GetUser();

    }
}
