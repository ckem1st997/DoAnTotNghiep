using KafKa.Net.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafKa.Net.IntegrationEvents.Events
{
    public class TestIntegrationEvent : IntegrationEvent
    {

        public string Username { get; set; }

    }
}