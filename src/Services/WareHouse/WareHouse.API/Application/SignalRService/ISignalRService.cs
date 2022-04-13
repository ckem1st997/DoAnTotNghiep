using System.Threading.Tasks;

namespace WareHouse.API.Application.SignalRService
{
    public interface ISignalRService
    {
        public Task<bool> SignalRChangByWareHouseBook(string id, string type);
    }
}
