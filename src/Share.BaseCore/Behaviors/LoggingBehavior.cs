using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using Share.BaseCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Share.BaseCore.Behaviors
{
    // log khi co Request
    // chạy đầu tiên sau đó đến các Behavior tiếp theo như cache, logging, validation, ...
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
       // private readonly IUserSevice _userSevice;
        private readonly DbContext _dbContext;


        public LoggingBehavior(DbContext context,ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
           // _userSevice = userSevice;
            _dbContext = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // nếu không connect được tới database thì ném ra ngoại lệ, không cho chạy vào các logic tiếp theo
            var checkConnectoDb = await _dbContext.Database.CanConnectAsync();
            if (!checkConnectoDb)
            {
                Log.Error("Can not connect to database");
                throw new ArgumentException(new Exception().Message);
            }
            //if (_userSevice != null && await _userSevice.GetUser() != null)
            //{
            //    var user = await _userSevice.GetUser();
            //    _logger.LogInformation("----- Handling command by {UserName}", user.UserName);
            //}
            request.GetType().GetProperties().ToList().ForEach(p =>
            {
                Log.Information("----- {PropertyName} : {PropertyValue}", p.Name, p.GetValue(request));
                //  _logger.LogInformation("----- {PropertyName} : {PropertyValue}", p.Name, p.GetValue(request));
            });
            Log.Information("----- Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);
            Log.Information("----- Handling command {CommandName} ({@Command})", "", request);
            var response = await next();
            Log.Information("----- Command {CommandName} handled - response: {@Response}", "", response);


            //_logger.LogInformation("----- Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);
            //_logger.LogInformation("----- Handling command {CommandName} ({@Command})", "", request);
            //var response = await next();
            //_logger.LogInformation("----- Command {CommandName} handled - response: {@Response}", "", response);

            return response;
        }
    }
}