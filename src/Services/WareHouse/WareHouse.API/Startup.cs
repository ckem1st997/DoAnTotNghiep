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
using Share.Base.Core.Filters;
using WareHouse.API.Infrastructure;
using Share.Base.Core;
using Share.Base.Core.Kafka;
using Share.Base.Core.Grpc;
using ShareImplemention;
using ShareImplemention.Background;
using Share.Base.Core.GraphQL;
using WareHouse.API.Infrastructure.GraphQL;
using WareHouse.Domain.Entity;
using WareHouse.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;
using WareHouse.API.Application.Validations.ConfigureServices;
using Share.Base.Service.Caching;
using Share.Base.Service.Configuration;
using Share.Base.Service.Middleware;
using Share.Base.Core.Extensions;

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
            services.AddConfigureAPI<Program>(Configuration);
            // local
            services.AddConfiguration(Configuration);
            services.AddApiGrpc<GrpcGetData.GrpcGetDataClient>(Configuration);
            services.AddServiceConfigImplementAPI();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new WareHouseModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            app.UseDeveloperExceptionPage();
            //  }

            //   app.UseHttpsRedirection();
            app.UseAPI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcGetDataWareHouseService>().EnableGrpcWeb();
                endpoints.MapControllers();
            });
            //local
            app.ConfigureEventBusKafka();

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