using GrpcGetDataToMaster;
using System.Threading.Tasks;

namespace WareHouse.API.Application.SignalRService
{
    public class SignalRService : ISignalRService
    {
        private readonly GrpcGetData.GrpcGetDataClient _client;

        public SignalRService(GrpcGetData.GrpcGetDataClient client)
        {
            _client = client;
        }

        public async Task<bool> SignalRChangByWareHouseBook(string id, string type)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new System.ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new System.ArgumentException($"'{nameof(type)}' cannot be null or empty.", nameof(type));
            }

            var res = await _client.CallChangeByWareHouseBookAsync(new BaseWareHouseBook { Id = id,Type=type });
            return res.Id.Equals(id);
        }
    }
}
