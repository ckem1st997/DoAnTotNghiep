using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Share.Base.Core.StackQueue;

namespace ShareImplemention.Background
{
    public sealed class StackHostedService : BackgroundService
    {
        private readonly IBackgroundTaskStack<StackModel> _taskQueue;
        private int IdBefore;

        public StackHostedService(
            IBackgroundTaskStack<StackModel> taskQueue) =>
            (_taskQueue) = (taskQueue);

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information(
                $"{nameof(QueueHostedService)} is running.{Environment.NewLine}" +
                $"{Environment.NewLine}Tap W to add a work item to the " +
                $"background Stack.{Environment.NewLine}");

            return ProcessTaskQueueAsync(stoppingToken);
        }

        private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("////////// Stack ////////////");
            int delay = 1;
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (await _taskQueue.CheckStack())
                    {
                        StackModel? workItem = await _taskQueue.Dequeue(stoppingToken);
                        Console.WriteLine("Stack: "+workItem?.Id);

                        //if (workItem?.Id is not null && workItem.Id > IdBefore)
                        //    IdBefore = workItem.Id;
                        //Console.WriteLine(IdBefore);
                    }
                    //else
                    // run câu lệnh update, get view từ data để so sánh, view<IdBefore thì update
                    //    Console.WriteLine("max: " + IdBefore);

                    // này sẽ delay
                    //  await Task.Delay(TimeSpan.FromMilliseconds(10), stoppingToken);
                    await Task.Delay(TimeSpan.FromMilliseconds(delay), stoppingToken);
                    //await Task.CompletedTask;
                    if (delay > 0)
                    {
                        Console.WriteLine(delay);
                        delay /= 2;

                    }

                }
                catch (OperationCanceledException)
                {
                    // Prevent throwing if stoppingToken was signaled
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error occurred executing task work item.");
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Log.Information(
                $"{nameof(StackHostedService)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
