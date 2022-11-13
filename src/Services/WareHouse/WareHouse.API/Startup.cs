using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.AutoMapper.ConfigureServices;
using WareHouse.API.Application.Validations.ConfigureServices;
using WareHouse.API.ConfigureServices.CustomConfiguration;
using Grpc.Net.Client.Web;
using GrpcGetDataToMaster;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WareHouse.API.Application.Authentication;
using Grpc.Net.ClientFactory;
using GrpcGetDataToWareHouse;
using WareHouse.API.IntegrationEvents;
using KafKa.Net;
using Nest;
using Elasticsearch.Net;
using Serilog;
using static Nest.ConnectionSettings;
using Newtonsoft.Json;
using Nest.JsonNetSerializer;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Share.BaseCore.Filters;
using Share.BaseCore.Cache;
using Share.BaseCore.Behaviors.ConfigureServices;
using WareHouse.API.Infrastructure;
using Share.BaseCore;
using Share.BaseCore.Authozire.ConfigureServices;
using Share.BaseCore.Kafka;
using Share.BaseCore.Grpc;
using ShareImplemention;

namespace WareHouse.API
{
    //Scaffold-DbContext "Server=tcp:127.0.0.1,5433;Initial Catalog=WarehouseManagement;Persist Security Info=True;User ID=sa;Password=Aa!0977751021;MultipleActiveResultSets = true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
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
            SetLicense();
          //  services.AddEasyCachingAPI(Configuration);
            services.AddCustomConfiguration(Configuration);
            services.AddMapper();
            services.AddValidator();
            services.AddBehavior();
            services.AddCache(Configuration);
            services.AddSingleton<IKafKaConnection, KafKaConnection>();
            services.AddEventBus(Configuration);
            services.AddApiElastic(Configuration);
            services.AddApiGrpc<GrpcGetData.GrpcGetDataClient>(Configuration);
            services.AddApiAuthentication();
            services.AddApiCors();
            services.AddServiceConfigImplementAPI();

            // services.AddSingleton<IHostedService, RequestTimeConsumer>();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.
            //   builder.ConfigureDBContext();
            builder.RegisterModule(new WareHouseModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WareHouse.API v1"));
            //  }

            //   app.UseHttpsRedirection();

            app.UseRouting();
            app.UseGrpcWeb();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcGetDataWareHouseService>().EnableGrpcWeb();
                endpoints.MapControllers();
            });
            app.ConfigureEventBusKafka();
            app.ConfigureRequestPipeline();

        }


        private static void SetLicense()
        {
            new Aspose.BarCode.License().SetLicense(AsposeHelper.BarCodeLicenseStream);
            new Aspose.Cells.License().SetLicense(AsposeHelper.CellsLicenseStream);
            new Aspose.Pdf.License().SetLicense(AsposeHelper.PdfLicenseStream);
            new Aspose.Words.License().SetLicense(AsposeHelper.WordsLicenseStream);
            // Fix tương thích trên .NET Core
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}