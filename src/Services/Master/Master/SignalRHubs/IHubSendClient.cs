using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Master.SignalRHubs
{
    public interface IHubSendCliend
    {
        Task SendMessageToCLient(object user, string name);
        Task WareHouseBookTrachkingToCLient(object user, string name);
    }
}
