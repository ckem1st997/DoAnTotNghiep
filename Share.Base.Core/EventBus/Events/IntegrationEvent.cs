using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Share.Base.Core.EventBus.Events
{
    //sự kiện tích hợp để đăng ký
    public class IntegrationEvent
    {

        //  [JsonInclude]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // [JsonInclude]
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
