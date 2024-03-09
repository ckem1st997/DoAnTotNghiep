using Autofac;
using Autofac.Extensions.DependencyInjection;
using Elastic.Apm.NetCoreAll;
using GrpcAuthMaster;
using GrpcGetDataToWareHouse;
using Master.ConfigureServices.Configuration;
using Master.IntegrationEvents;
using Master.SignalRHubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Share.Base.Core.AutoDependencyInjection;
using Share.Base.Core.Grpc;
using Share.Base.Service.Configuration;

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
            services.AddConfigureAPI<Program>(Configuration);
            services.AddConfiguration(Configuration);
            // setup local
            services.AddEventBusKafka();
            // services.AddHostedService<RequestTimeConsumer>();
            // call http to grpc

            services.AddApiGrpc<GrpcGetDataWareHouse.GrpcGetDataWareHouseClient>(Configuration.GetValue<string>("Grpc:Port"));
            services.Configure<PasswordHasherOptions>(option =>
            {
                option.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3;
                option.IterationCount = 12000;
            });

            // services.AddApiAuthentication();
            //  services.AddApiCors();
            services.AddSignalR(options =>
            {
                // Global filters will run first
                options.AddFilter<CustomFilter>();
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterDIContainer();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //  if (env.IsDevelopment())
            //  {
            app.UseDeveloperExceptionPage();
            app.ConfigureSwagger();
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
          //  app.UseAllElasticApm(Configuration);

        }
    }
}
