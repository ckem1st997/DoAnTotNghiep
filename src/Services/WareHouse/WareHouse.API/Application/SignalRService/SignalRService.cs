using GrpcGetDataToMaster;
using Share.Base.Core.AutoDependencyInjection.InjectionAttribute;
using System.Threading.Tasks;

namespace WareHouse.API.Application.SignalRService
{
    [ScopedDependency]
    public class SignalRService : ISignalRService
    {
        private readonly GrpcGetData.GrpcGetDataClient _client;

        public SignalRService(GrpcGetData.GrpcGetDataClient client)
        {
            _client = client;
        }

        public async Task<string> SignalRChangByWareHouseBook(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new System.ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            }
            var res = await _client.CallChangeByWareHouseBookAsync(new BaseId { Id = id});
            return res.Id;
        }
    }
}
