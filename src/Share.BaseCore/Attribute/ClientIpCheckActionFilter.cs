using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Share.BaseCore.Authozire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Attribute
{
    /// <summary>
    /// ignore ip not in list ip
    /// [ServiceFilter(typeof(ClientIpCheckActionFilter))]
    /// </summary>
    /// 
    //    services.AddScoped<ClientIpCheckActionFilter>(container =>
    //{
    //    var loggerFactory = container.GetRequiredService<ILoggerFactory>();
    //    var logger = loggerFactory.CreateLogger<ClientIpCheckActionFilter>();

    //    return new ClientIpCheckActionFilter(
    //        Configuration["AdminSafeList"], logger);
    //});
    public class ClientIpCheckActionFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        private readonly string _safelist;

        public ClientIpCheckActionFilter(string safelist, ILogger logger)
        {
            _safelist = safelist;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
            _logger.LogDebug("Remote IpAddress: {RemoteIp}", remoteIp);
            var ip = _safelist.Split(';');
            var badIp = true;
            IAuthozireExtensionForMaster iAuthenForMaster = EngineContext.Current.Resolve<IAuthozireExtensionForMaster>();

            if (remoteIp.IsIPv4MappedToIPv6)
            {
                remoteIp = remoteIp.MapToIPv4();
            }
            var testIp = IPAddress.Parse(iAuthenForMaster.GetClaimType("IpAddress"));

            if (testIp.Equals(remoteIp))
            {
                badIp = false;
            }

            //foreach (var address in ip)
            //{
            //    var testIp = IPAddress.Parse(address);

            //    if (testIp.Equals(remoteIp))
            //    {
            //        badIp = false;
            //        break;
            //    }
            //}

            if (badIp)
            {
                _logger.LogWarning("Forbidden Request from IP: {RemoteIp}", remoteIp);
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
