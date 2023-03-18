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
            var configuration = HostAPI.GetConfiguration();

            Log.Logger = HostAPI.CreateSerilogLogger(configuration, "Master");
            HostAPI.LogStartUp<Startup>(args, configuration.GetValue("PORT", 80), configuration.GetValue("GRPC_PORT", 81));

            //Log.Information("Starting up");
            //CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //            .ConfigureLogging(logging =>
        //            {
        //                logging.AddFilter("Grpc", LogLevel.Debug);
        //            })
        //         .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.ConfigureKestrel(options =>
        //            {
        //                options.Listen(IPAddress.Any, 50001, listenOptions =>
        //                {
        //                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
        //                });

        //                options.Listen(IPAddress.Any, 50000, listenOptions =>
        //                {
        //                    listenOptions.Protocols = HttpProtocols.Http2;

        //                });
        //            });
        //            //   }
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}
