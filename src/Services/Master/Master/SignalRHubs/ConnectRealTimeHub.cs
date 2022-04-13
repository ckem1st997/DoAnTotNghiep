using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Master.SignalRHubs
{
    public class ConnectRealTimeHub : Hub<IHubSendCliend>
    {
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendMessageToCLient(user, message);
        //}
    }
}