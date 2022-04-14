using System.Threading.Tasks;

namespace Master.SignalRHubs
{
    public interface IConnectRealTimeHub
    {
        public Task WareHouseBookTrachking(object data,string name);
    }
}
