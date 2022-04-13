using Master.Controllers.BaseController;
using Master.SignalRHubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Master.Controllers
{
    public class RealTimeController : BaseControllerMaster
    {
        private readonly IHubContext<ConnectRealTimeHub, IHubSendCliend> _hubContext;
        public RealTimeController(IHubContext<ConnectRealTimeHub, IHubSendCliend> hubContext)
        {
            _hubContext = hubContext;
        }


        [AllowAnonymous]
        [Route("connect")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync(string iii)
        {
            await _hubContext.Clients.All.SendMessageToCLient(iii, "test");
            return Ok();
        }
    }
}
