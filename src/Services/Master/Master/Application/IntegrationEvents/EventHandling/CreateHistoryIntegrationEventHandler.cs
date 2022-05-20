using Base.Events;
using Infrastructure;
using KafKa.Net.Abstractions;
using KafKa.Net.Events;
using Master.Application.Message;
using Master.Service;
using Master.SignalRHubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.IntegrationEvents
{
    // lớp xử lý event nhận by Kafka
    public class CreateHistoryIntegrationEventHandler : IIntegrationEventHandler<CreateHistoryIntegrationEvent>
    {
        private readonly ILogger<CreateHistoryIntegrationEventHandler> _logger;
        private readonly MasterdataContext _masterdataContext;
        private readonly IHubContext<ConnectRealTimeHub> _hubContext;

        public CreateHistoryIntegrationEventHandler(IHubContext<ConnectRealTimeHub> hubContext,MasterdataContext masterdataContext, ILogger<CreateHistoryIntegrationEventHandler> logger)
        {
            _logger = logger;
            _masterdataContext = masterdataContext;
            _hubContext = hubContext;
        }

        public async Task Handle(CreateHistoryIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at UserAPI - ({@IntegrationEvent})", @event.Id, @event);
                var request = @event;
                var model = new HistoryNotication()
                {
                    Id = request.Id,
                    Body = request.Body,
                    CreateDate = DateTime.Now,
                    Link = request.Link,
                    Method = request.Method,
                    OnDelete = false,
                    Read = false,
                    UserName = request.UserName,
                };
                await _masterdataContext.AddAsync(model);
                var res = await _masterdataContext.SaveChangesAsync();
                _logger.LogInformation("Result to event: " + (res > 0).ToString() + "");
                if(res > 0)
                {
                    var ress = new ResultMessageResponse()
                    {
                        data = request.UserName,
                        success = res>0
                    };
                    await _hubContext.Clients.All.SendAsync("HistoryTrachkingToCLient", ress, request.UserName);
                }    
            }

        }
    }
}