using AutoMapper;
using KafKa.Net.Abstractions;
using KafKa.Net.IntegrationEvents;
using KafKa.Net.IntegrationEvents.Events;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Commands.Create;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Querie.CheckCode;
using WareHouse.API.Application.SignalRService;
using WareHouse.API.Application.Validations.BeginningWareHouse;

namespace WareHouse.API.IntegrationEvents.EventHandling
{
    // [Authorize]
    internal class InwardIntegrationEventHandler : IIntegrationEventHandler<InwardIntegrationEvent>
    {
        private readonly ILogger<InwardIntegrationEventHandler> _logger;
        private readonly IMediator _mediat;
        private readonly IMapper _mapper;
        private readonly IUserSevice _userSevice;
        private readonly ISignalRService _signalRService;
        public InwardIntegrationEventHandler(IMapper mapper, ISignalRService signalRService, IUserSevice userSevice, IMediator mediat, ILogger<InwardIntegrationEventHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _mapper = mapper;
            _userSevice = userSevice;
            _signalRService = signalRService;
        }

        public async Task Handle(InwardIntegrationEvent @event)
        {
            using (LogContext.PushProperty("IntegrationEventContext", $"{@event.Id}"))
            {
                _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at UserAPI - ({@IntegrationEvent})", @event.Id, @event);

                var inwardCommands = _mapper.Map<InwardCommands>(@event);
                var validator = new InwardCommandValidator();
                var result = validator.Validate(inwardCommands);
                if (!result.IsValid)
                    _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at UserAPI - ({@IntegrationEvent}) dữ liệu đầu vào không đúng", @event.Id, @event);
                else
                {
                    inwardCommands.CreatedDate = DateTime.Now;
                    inwardCommands.ModifiedDate = DateTime.Now;
                    foreach (var item in inwardCommands.InwardDetails)
                    {
                        item.Amount = item.Uiquantity * item.Uiprice;
                        int convertRate = await _mediat.Send(new GetConvertRateByIdItemCommand() { IdItem = item.ItemId, IdUnit = item.UnitId });
                        item.Quantity = convertRate * item.Uiquantity;
                        item.Price = item.Amount;
                    }
                    var data = await _mediat.Send(new CreateInwardCommand() { InwardCommands = inwardCommands });
                    if (data)
                        _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at UserAPI - ({@IntegrationEvent}) thành công", @event.Id, @event);
                    else
                        _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at UserAPI - ({@IntegrationEvent}) thất bại", @event.Id, @event);



                }

            }

        }
    }
}