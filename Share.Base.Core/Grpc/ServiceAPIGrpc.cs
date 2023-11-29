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

namespace Share.Base.Core.Grpc
{
    public static class ServiceAPIGrpc
    {
        /// <summary>
        /// GrpcGetData.GrpcGetDataClient
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddApiGrpc<T>(this IServiceCollection services, string post) where T : class
        {
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
                options.Interceptors.Add<ServerLoggerInterceptor>();
            });
            services.AddTransient<GrpcExceptionInterceptor>();
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            services.AddGrpcClient<T>(o =>
            {
                o.Address = new Uri(post);
            }).AddInterceptor<GrpcExceptionInterceptor>(InterceptorScope.Client)
                .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(httpHandler));
        }
    }
}
