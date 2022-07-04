using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.Formatting.Json;
using System.Runtime.InteropServices;
using Autofac.Extensions.DependencyInjection;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace WareHouse.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();

            Log.Logger = CreateSerilogLogger(configuration);
            Log.Information("Starting up");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                 Host.CreateDefaultBuilder(args)
                         .ConfigureLogging(logging =>
                         {
                             logging.AddFilter("Grpc", LogLevel.Debug);
                         })
                  .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                     .ConfigureWebHostDefaults(webBuilder =>
                     {
                         //webBuilder.ConfigureKestrel(options =>
                         //{
                         //    options.ConfigureEndpointDefaults(defaults =>defaults.Protocols = HttpProtocols.Http2);
                         //});
                         //   if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                         //   {
                         webBuilder.ConfigureKestrel(options =>
                         {
                             options.Listen(IPAddress.Any, 5005, listenOptions =>
                                 {
                                     listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                                 });

                             options.Listen(IPAddress.Any, 5006, listenOptions =>
                             {
                                 listenOptions.Protocols = HttpProtocols.Http2;
                             });
                         });
                         //     }
                         webBuilder.UseStartup<Startup>();
                     });


        static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {

            var seqServerUrl = configuration["Serilog:SeqServerUrl"];
            var logstashUrl = configuration["Serilog:LogstashgUrl"];
            Console.WriteLine(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:5044" : logstashUrl);
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", "WareHouse")
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl, apiKey: "0QEfAbE4THZTcUu6I7bQ")
               //.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:5044" : logstashUrl))
               //{
               //    ModifyConnectionSettings = x => x.BasicAuthentication("logstash_internal", "changeme"),
               //    AutoRegisterTemplate = true,
               //    IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{DateTime.UtcNow:yyyy-MM}"
               //})
               // cần phải connect đến logstash trước khi create index to kibana
               .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:5044" : logstashUrl)
               .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            return builder.Build();
        }
    }
}
