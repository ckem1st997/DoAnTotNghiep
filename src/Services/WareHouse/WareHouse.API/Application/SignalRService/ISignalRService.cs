using System.Threading.Tasks;

namespace WareHouse.API.Application.SignalRService
{
    public interface ISignalRService
    {
        public Task<string> SignalRChangByWareHouseBook(string id);
    }
}
