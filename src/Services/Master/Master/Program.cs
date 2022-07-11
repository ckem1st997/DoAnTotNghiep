using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Master
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
                    //    options.ConfigureEndpointDefaults(defaults => defaults.Protocols = HttpProtocols.Http2);
                    //});
                    //if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    //{
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.Listen(IPAddress.Any, 50001, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                        });

                        options.Listen(IPAddress.Any, 50000, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http2;

                        });
                    });
                    //   }
                    webBuilder.UseStartup<Startup>();
                });


        static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {

            var seqServerUrl = configuration["Serilog:SeqServerUrl"];
            var logstashUrl = configuration["Serilog:LogstashgUrl"];
            Console.WriteLine(seqServerUrl + " " + logstashUrl);
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", "Master")
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl, apiKey: "0QEfAbE4THZTcUu6I7bQ")
               .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:5044" : logstashUrl)
               .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        static IConfiguration GetConfiguration()
        {
            var appjon = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals(Environments.Production) ? "appsettings.Production.json" : "appsettings.Development.json";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            return builder.Build();
        }
    }
}
