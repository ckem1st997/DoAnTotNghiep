using Base.Events;
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

        /// <summary>
        /// Thêm mới lịch sử
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Method"></param>
        /// <param name="Body"></param>
        /// <param name="Read"></param>
        /// <param name="Link"></param>
        /// <returns></returns>
        public Task<bool> CreateHistory(CreateHistoryIntegrationEvent create);

        public Task<bool> ActiveHistory(string UserName);

    }
}
