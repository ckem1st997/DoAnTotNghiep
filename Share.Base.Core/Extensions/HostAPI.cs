using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Core.Extensions
{
    public static class HostAPI
    {
        private static string getEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;

        public static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration, string nameApp)
        {

            var seqServerUrl = configuration["Serilog:SeqServerUrl"];
            var logstashUrl = configuration["Serilog:LogstashgUrl"];
            return new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .Enrich.WithProperty("ApplicationContext-" + getEnv, nameApp)
                    .Enrich.FromLogContext()
                   .WriteTo.Console()
                   .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:5044" : logstashUrl, queueLimitBytes: 1000000)
                   .ReadFrom.Configuration(configuration)
                    .CreateLogger();
        }

        public static IConfiguration GetConfiguration()
        {
            var appjon = !string.IsNullOrEmpty(getEnv) && getEnv.Equals(Environments.Production) ? "appsettings.Production.json" : "appsettings.Development.json";
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{getEnv ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

        public static void LogStartUp<T>(string[] args, int portHttp1AndHttp2, int portHttp2) where T : class
        {
            Log.Information("Starting up: " + getEnv);
            CreateHostBuilder<T>(args, portHttp1AndHttp2, portHttp2).Build().Run();
            WebApplication.CreateBuilder(new WebApplicationOptions
            {
                EnvironmentName = getEnv
            });
        }

        public static IHostBuilder CreateHostBuilder<T>(string[] args, int portHttp1AndHttp2, int portHttp2) where T : class =>
                Host.CreateDefaultBuilder(args)
                        .ConfigureLogging(logging =>
                        {
                            logging.AddFilter("Grpc", LogLevel.Information);
                        })
                 .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.ConfigureKestrel(options =>
                        {
                            options.Listen(IPAddress.Any, portHttp1AndHttp2, listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                            });

                            options.Listen(IPAddress.Any, portHttp2, listenOptions =>
                            {
                                listenOptions.Protocols = HttpProtocols.Http2;
                            });
                        });
                        webBuilder.UseStartup<T>();
                    }).UseSerilog();

    }
}
