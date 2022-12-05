using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Share.BaseCore.StackAndQueue;
using ShareModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareImplemention.Background
{
    public sealed class QueueHostedService : BackgroundService
    {
        private readonly IBackgroundTaskQueue<QueueModel> _taskQueue;
        private int IdBefore;

        public QueueHostedService(
            IBackgroundTaskQueue<QueueModel> taskQueue,
            ILogger<QueueHostedService> logger) =>
            (_taskQueue) = (taskQueue);

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information(
                $"{nameof(QueueHostedService)} is running.{Environment.NewLine}" +
                $"{Environment.NewLine}Tap W to add a work item to the " +
                $"background queue.{Environment.NewLine}");

            return ProcessTaskQueueAsync(stoppingToken);
        }

        private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("////////// Queue ////////////");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    QueueModel? workItem = await _taskQueue.DequeueAsync(stoppingToken);
                    if (workItem?.Id is not null && workItem.Id > IdBefore)
                        IdBefore = workItem.Id;
                    // Console.WriteLine("Queue : " + workItem.Name);
                    Console.WriteLine("Queue: " + workItem?.Id);
                    // Console.WriteLine(IdBefore);

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
            Log.Information($"{nameof(QueueHostedService)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }

}
