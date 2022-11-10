
using Share.BaseCore.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Events
{
    public class CreateHistoryIntegrationEvent : IntegrationEvent
    {
        public string UserName { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public bool Read { get; set; }
        public string Link { get; set; }
    }
}
