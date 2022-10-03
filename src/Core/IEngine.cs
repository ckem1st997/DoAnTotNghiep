using System;
using System.Collections.Generic;
using Autofac.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    /// <summary>
    /// Classes implementing this interface can serve as a portal for the various services composing the Nop engine. 
    /// Edit functionality, modules and implementations access most Nop functionality through this interface.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Configure HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        void ConfigureRequestPipeline(IApplicationBuilder application);

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <param name="scope">Scope</param>
        /// <typeparam name="T">Type of resolved service</typeparam>
        /// <returns>Resolved service</returns>
        T Resolve<T>(string name = null, params Parameter[] parameters) where T : class;

        object Resolve(Type type, string name = null, params Parameter[] parameters);

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">Type of resolved services</typeparam>
        /// <returns>Collection of resolved services</returns>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// Resolve unregistered service
        /// </summary>
        /// <param name="type">Type of service</param>
        /// <returns>Resolved service</returns>
        object ResolveUnregistered(Type type);

    }
}