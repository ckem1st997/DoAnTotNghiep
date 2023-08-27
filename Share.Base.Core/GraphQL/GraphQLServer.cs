using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Share.Base.Core.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Core.GraphQL
{
    public static class GraphQLServer
    {
        //public static void AddGraphQLServer<T, TContext>(this IServiceCollection services) where T : class where TContext : DbContext
        //{
        //    services.AddGraphQLServer().AddQueryType<T>().AddHttpRequestInterceptor<HttpRequestInterceptor>().AddFiltering()
        //.AddSorting()
        //.AddProjections()
        // .InitializeOnStartup()
        // .RegisterDbContext<TContext>();
        //}
        public static void AppGraphQLServer(this IApplicationBuilder app)
        {
            //app.UseGraphQL("/graphql");            // url to host GraphQL endpoint
            //app.UseGraphQLPlayground(
            //    "/",                               // url to host Playground at
            //    new PlaygroundOptions
            //    {
            //        GraphQLEndPoint = "/graphql",         // url of GraphQL endpoint
            //        SubscriptionsEndPoint = "/graphql",   // url of GraphQL endpoint
            //    });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/graphql");
            });
        }
    }
}
