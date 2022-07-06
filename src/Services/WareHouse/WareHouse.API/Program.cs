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
//input
//{
//    http {
//#default host 0.0.0.0:8080
//        codec => json

//    }
//# jdbc {
//# jdbc_validate_connection => false
//# jdbc_connection_string => "jdbc:sqlserver://localhost:5433;databaseName=WarehouseManagement;"
//# jdbc_user => "sa"
//# jdbc_password => "Aa!0977751021"
//# jdbc_driver_library => "/usr/share/logstash/logstash-core/lib/jars/mssqll-jdbc-10.2.1.jre8.jar"
//# jdbc_driver_class => "com.microsoft.sqlserver.jdbc.SQLServerDriver"
//# statement => "SELECT TOP(1000) * FROM WareHouse"
//# tracking_column => "id"
//# tracking_column_type => "numeric"
//# use_column_value => true		
//# last_run_metadata_path => "/usr/share/logstash/pipeline/.logstash_jdbc_last_run"
//# schedule => "*/10 * * * * *"
//# clean_run => true
//#   # tags => "sqlserver"
//# }
//}

//## Add your filters / logstash plugins configuration here
//filter
//{
//    split {
//        field => "events"

//        target => "e"

//        remove_field => "events"

//    }

//}

//output
//{
//#if "sqlserver" in [tags]
//# {
//	elasticsearch {
//		hosts => "elasticsearch:9200"
//		index=>"doantotnghiep-%{+xxxx.ww}"
//# index=>"sqlserver"


//# document_id => "%{Id}"
//# doc_as_upsert => true
//# action => "update"
//	}
//# stdout { codec => rubydebug }
//#   }
//#else
//# {
//# elasticsearch {
//# hosts => "elasticsearch:9200"
//# index=>"searchmssql-%{+xxxx.ww}"
//#   }
//#}

//}


//# The TCP/IP connection to the host localhost, port 5433 has failed. Error: 
//#  "Connection refused (Connection refused). Verify the connection properties.
//# Make sure that an instance of SQL Server is running on the host and accepting TCP/IP connections at the port.
//# Make sure that TCP connections to the port are not blocked by a firewall.".


//RUN logstash-plugin install logstash-input-http
//RUN logstash-plugin install logstash-input-jdbc
//# RUN rm -f /usr/share/logstash/pipeline/logstash.conf
//USER root 

//COPY mssql-jdbc-10.2.1.jre8.jar /usr/share/logstash/logstash-core/lib/jars/mssql-jdbc-10.2.1.jre8.jar
