using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WareHouse.API.ConfigureServices.CustomConfiguration;
using GrpcGetDataToMaster;
using System.Text;
using GrpcGetDataToWareHouse;
using WareHouse.API.IntegrationEvents;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using WareHouse.API.Infrastructure;
using Share.Base.Core.Grpc;
using Share.Base.Service.Configuration;
using Share.Base.Core.Extensions;
using Share.Base.Core.AutoDependencyInjection;
using Elastic.Apm.NetCoreAll;

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
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new WareHouseModule());
            builder.RegisterDIContainer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            //   app.UseHttpsRedirection();
            app.UseAPI();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GrpcGetDataWareHouseService>().EnableGrpcWeb();
                endpoints.MapControllers();
            });
            //local
            app.ConfigureEventBusKafka();
            app.UseAllElasticApm(Configuration);

        }


        public static void SetLicense()
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