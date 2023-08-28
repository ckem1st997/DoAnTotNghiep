using Autofac;
using Autofac.Extensions.DependencyInjection;
using Grpc.Net.Client.Web;
using Grpc.Net.ClientFactory;
using GrpcGetDataToMaster;
using GrpcGetDataToWareHouse;
using Master.ConfigureServices.Configuration;
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
using Share.Base.Core.Grpc;
using Share.Base.Core.Kafka;
using Share.Base.Service.Caching;
using Share.Base.Service.Configuration;
using Share.Base.Service.Security.ConfigureServices;
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
        public ILifetimeScope AutofacContainer { get; private set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCache(Configuration);
            services.AddControllers();
            services.AddConfiguration(Configuration);
            services.AddSingleton<IKafKaConnection, KafKaConnection>();
            services.AddEventBus(Configuration);
            services.AddEventBusKafka();
            services.AddHostedService<RequestTimeConsumer>();
            // call http to grpc
            services.AddApiGrpc<GrpcGetDataWareHouse.GrpcGetDataWareHouseClient>(Configuration);
            services.Configure<PasswordHasherOptions>(option =>
            {
                option.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
                option.IterationCount = 12000;
            });
            services.AddApiAuthentication();
            services.AddApiCors();
            services.AddSignalR(options =>
            {
                // Global filters will run first
                options.AddFilter<CustomFilter>();
            });

        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //  if (env.IsDevelopment())
            //  {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
            //  }
            app.UseHttpsRedirection();
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
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
          //  app.ConfigureEventBusKafka();
            app.ConfigureRequestPipeline();
        }


    }
}
