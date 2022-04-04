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
using WareHouse.API.Application.Behaviors.ConfigureServices;
using WareHouse.API.Application.Cache;
using WareHouse.API.Application.Validations.ConfigureServices;
using WareHouse.API.ConfigureServices.CustomConfiguration;
using Grpc.Net.Client.Web;
using GrpcGetDataToMaster;
using System.Net;
using System.Net.Http;
using System.Reflection;
using WareHouse.API.Filters;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCustomConfiguration(Configuration);
            services.AddMapper();
            services.AddValidator();
            services.AddBehavior();
            services.AddCache(Configuration);
            services.AddApiVersioning(x =>
            {
                // setup ApiVersion v1 
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
                //    x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .WithOrigins("http://localhost:4200", "http://localhost:55671")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });


            // send log to seq by Microsoft.Extensions.Logging
            services.AddLogging(loggingBuilder =>
            {
                //   loggingBuilder.UseSerilog(Configuration);
                var seqServerUrl = Configuration["Serilog:SeqServerUrl"];

                loggingBuilder.AddSeq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl, apiKey: "0QEfAbE4THZTcUu6I7bQ");
            });

            services.AddTransient<GrpcExceptionInterceptor>();

            services.AddGrpcClient<GrpcGetData.GrpcGetDataClient>(o =>
           {
               o.Address = new Uri("https://localhost:5001");
               // o.Address = new Uri("https://apiproducts97.azurewebsites.net");
           }).AddInterceptor<GrpcExceptionInterceptor>().ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WareHouse.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
