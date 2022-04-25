using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KafKa.Net.Events
{
    //sự kiện tích hợp để đăng ký
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid().ToString();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(string id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        [JsonInclude]
        public string Id { get; private init; }

        [JsonInclude]
        public DateTime CreationDate { get; private init; }
    }
}
