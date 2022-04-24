using KafKa.Net.Abstractions;
using KafKa.Net.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafKa.Net.IntegrationEvents.EventHandling
{
    internal class TestIntegrationEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
    {
        private readonly ILogger<TestIntegrationEventHandler> _logger;
        public TestIntegrationEventHandler (ILogger<TestIntegrationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(TestIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at UserAPI - ({@IntegrationEvent})", @event.Id, @event);
                Console.WriteLine(@event.Username);
            }

        }
    }
}