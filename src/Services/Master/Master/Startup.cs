using Grpc.Net.Client.Web;
using Grpc.Net.ClientFactory;
using GrpcGetDataToMaster;
using GrpcGetDataToWareHouse;
using KafKa.Net;
using Master.Application.Authentication;
using Master.ConfigureServices.CustomConfiguration;
using Master.Extension;
using Master.IntegrationEvents;
using Master.Models;
using Master.Service;
using Master.SignalRHubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Share.BaseCore.Authozire.ConfigureServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Master
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCustomConfiguration(Configuration);
            services.InitAppSettings(Configuration);
            services.AddTransient<GrpcExceptionInterceptor>();
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;

            });
            services.AddSingleton<IKafKaConnection, KafKaConnection>();
            services.AddEventBus(Configuration);
            services.AddHostedService<RequestTimeConsumer>();

            // call http to grpc
            AppContext.SetSwitch(
  "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            services.AddGrpcClient<GrpcGetDataWareHouse.GrpcGetDataWareHouseClient>(o =>
            {
                o.Address = new Uri(Configuration.GetValue<string>("Grpc:Port"));
            }).AddInterceptor<GrpcExceptionInterceptor>(InterceptorScope.Client).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(httpHandler));

            services.Configure<PasswordHasherOptions>(option =>
            {
                option.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
                option.IterationCount = 12000;
            });

            
            services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();
            services.AddApiAuthentication();
            services.AddApiCors();
            services.AddSignalR(options =>
            {
                // Global filters will run first
                options.AddFilter<CustomFilter>();
            });
            services.AddLogging(loggingBuilder =>
            {
                //   loggingBuilder.UseSerilog(Configuration);
                var seqServerUrl = Configuration["Serilog:SeqServerUrl"];

                loggingBuilder.AddSeq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl,
                    apiKey: "0QEfAbE4THZTcUu6I7bQ");
            });
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          //  if (env.IsDevelopment())
          //  {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasicAuth v1"));
          //  }
            //  app.UseHttpsRedirection();

            app.UseRouting();
            app.UseGrpcWeb();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcGetDataToMasterService>().EnableGrpcWeb();
                endpoints.MapControllers();
                endpoints.MapHub<ConnectRealTimeHub>("/signalr");
            });
            app.ConfigureEventBus();
        }


    }
}
