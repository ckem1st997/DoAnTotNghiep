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
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Serilog.Events;
using Share.Base.Core.Extensions;

namespace WareHouse.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = HostAPI.GetConfiguration();

            Log.Logger = HostAPI.CreateSerilogLogger(configuration, "WareHouse");
            HostAPI.LogStartUp<Startup>(args,configuration.GetValue("PORT", 80), configuration.GetValue("GRPC_PORT", 81));
        }


        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //         Host.CreateDefaultBuilder(args)
        //                 .ConfigureLogging(logging =>
        //                 {
        //                     logging.AddFilter("Grpc", LogLevel.Debug);
        //                 })
        //          .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //             .ConfigureWebHostDefaults(webBuilder =>
        //             {
        //                 webBuilder.ConfigureKestrel(options =>
        //                 {
        //                     options.Listen(IPAddress.Any, 5005, listenOptions =>
        //                         {
        //                             listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
        //                         });

        //                     options.Listen(IPAddress.Any, 5006, listenOptions =>
        //                     {
        //                         listenOptions.Protocols = HttpProtocols.Http2;
        //                     });
        //                 });
        //                 //     }
        //                 webBuilder.UseStartup<Startup>();
        //             });



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
