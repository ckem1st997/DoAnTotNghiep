using System.Threading.Tasks;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Authentication
{
    public interface IUserSevice
    {
        public Task<UserGrpc> GetUser();


        /// <summary>
        /// true nếu có quyền
        /// </summary>
        /// <param name="idWareHouse">mã kho cần kiểm tra</param>
        /// <returns></returns>
        public Task<bool> CheckWareHouseIdByUser(string idWareHouse);

    }
}
