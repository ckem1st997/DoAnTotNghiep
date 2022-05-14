using KafKa.Net.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Authentication;
using WareHouse.Infrastructure;

namespace WareHouse.API.Application.Behaviors
{
    // log khi co Request
    // chạy đầu tiên sau đó đến các Behavior tiếp theo như cache, logging, validation, ...
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IUserSevice _userSevice;
        private readonly WarehouseManagementContext _dbContext;


        public LoggingBehavior(WarehouseManagementContext dbContext, IUserSevice userSevice, ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
            _userSevice = userSevice;
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // nếu không connect được tới database thì ném ra ngoại lệ, không cho chạy vào các logic tiếp theo
            var checkConnectoDb = await _dbContext.Database.CanConnectAsync();
            if (!checkConnectoDb)
            {
                _logger.LogError("Can not connect to database");
                throw new Exception("Can not connect to database");
            }
            if (_userSevice != null && await _userSevice.GetUser() != null)
            {
                var user = await _userSevice.GetUser();
                _logger.LogInformation("----- Handling command by {UserName}", user.UserName);
            }
            request.GetType().GetProperties().ToList().ForEach(p =>
            {
                _logger.LogInformation("----- {PropertyName} : {PropertyValue}", p.Name, p.GetValue(request));
            });
            _logger.LogInformation("----- Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);
            _logger.LogInformation("----- Handling command {CommandName} ({@Command})", "", request);
            var response = await next();
            _logger.LogInformation("----- Command {CommandName} handled - response: {@Response}", "", response);

            return response;
        }
    }
}