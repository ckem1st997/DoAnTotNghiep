using Grpc.Core;
using Grpc.Net.Client.Web;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Grpc
{
    public static class ServiceAPIGrpc
    {
        public static void AddApiGrpc<T>(this IServiceCollection services,IConfiguration configuration) where T : class
        {
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;

            });
            services.AddTransient<GrpcExceptionInterceptor>();
            AppContext.SetSwitch(
  "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            services.AddGrpcClient<T>(o =>
            {
                o.Address = new Uri(configuration.GetValue<string>("Grpc:Port"));
            }).AddInterceptor<GrpcExceptionInterceptor>(InterceptorScope.Client)
                .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(httpHandler));
        }
    }
}
