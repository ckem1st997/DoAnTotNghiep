using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Middleware
{
    /// <summary>
    /// app.UseMiddleware<AdminSafeListMiddleware>(Configuration["AdminSafeList"]);
    /// </summary>
    public class AdminSafeListMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly byte[][] _safelist;

        public AdminSafeListMiddleware(
            RequestDelegate next,
            string safelist)
        {
            var ips = safelist.Split(';');
            _safelist = new byte[ips.Length][];
            for (var i = 0; i < ips.Length; i++)
            {
                _safelist[i] = IPAddress.Parse(ips[i]).GetAddressBytes();
            }

            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method != HttpMethod.Get.Method)
            {
                var remoteIp = context.Connection.RemoteIpAddress;
                Log.Debug("Request from Remote IP address: {RemoteIp}", remoteIp);

                var bytes = remoteIp.GetAddressBytes();
                var badIp = true;
                foreach (var address in _safelist)
                {
                    if (address.SequenceEqual(bytes))
                    {
                        badIp = false;
                        break;
                    }
                }

                if (badIp)
                {
                    Log.Debug(
                        "Forbidden Request from Remote IP address: {RemoteIp}", remoteIp);
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }

    public class RemoteIpAddressMiddleware
    {
        private readonly RequestDelegate _next;

        public RemoteIpAddressMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var remoteIp = context.Connection.RemoteIpAddress;
            Log.Information("Request from Remote IP address: {RemoteIp}", remoteIp);

            await _next.Invoke(context);
        }
    }

}