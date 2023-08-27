using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using Serilog;

namespace Share.Base.Service.Behaviors
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        //private readonly Stopwatch _timer;
        //private readonly ILogger<TRequest> _logger;
        //private readonly IUserSevice _userSevice;


        //public PerformanceBehaviour(
        //    ILogger<TRequest> logger, IUserSevice userSevice)
        //{
        //    _timer = new Stopwatch();
        //    _logger = logger;
        //    _userSevice = userSevice;
        //}

        //public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        //{
        //    _timer.Start();

        //    var response = await next();

        //    _timer.Stop();

        //    var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        //    if (elapsedMilliseconds > 500)
        //    {
        //        var requestName = typeof(TRequest).Name;
        //        var user = await _userSevice.GetUser();
        //        var userId = user.Id ?? string.Empty;
        //        var userName = string.Empty;

        //        if (!string.IsNullOrEmpty(userId))
        //        {
        //            userName = user.UserName;
        //        }

        //        Log.Information("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
        //            requestName, elapsedMilliseconds, userId, userName, request);
        //    }

        //    return response;
        //}
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            throw new NotImplementedException();
        }
    }
}