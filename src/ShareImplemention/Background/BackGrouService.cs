﻿using Microsoft.Extensions.DependencyInjection;
using Share.BaseCore.Cache.CacheName;
using Share.BaseCore.StackAndQueue;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareImplemention.Background
{
    public static class BackGrouService
    {
        public static void AddBackGrouService(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IBackgroundTaskQueue<>), typeof(DefaultBackgroundTaskQueue<>));
            services.AddSingleton(typeof(IBackgroundTaskStack<>), typeof(DefaultBackgroundTaskStack<>));
            //   services.AddHostedService<QueueHostedService>();
            //  services.AddHostedService<StackHostedService>();
            services.AddHostedService<QueueHostedTaskService>();
        }


    }
}