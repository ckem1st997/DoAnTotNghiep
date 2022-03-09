using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace WareHouse.API.Application.Extensions
{
    public class JsonTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        public override void SetValue(IDbDataParameter parameter, T value)
        {
            parameter.Value = JsonConvert.SerializeObject(value);
        }

        public override T Parse(object value)
        {
            if (value is string json)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }

            return  JsonConvert.DeserializeObject<T>(default);
        }
    }
}
