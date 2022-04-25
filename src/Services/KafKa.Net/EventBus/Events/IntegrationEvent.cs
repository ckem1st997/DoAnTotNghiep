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

      //  [JsonInclude]
        public string Id { get; set; }

       // [JsonInclude]
        public DateTime CreationDate { get; set; }
    }
}
