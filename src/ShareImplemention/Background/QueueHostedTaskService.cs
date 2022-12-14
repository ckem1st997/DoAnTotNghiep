using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Share.BaseCore.StackAndQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareImplemention.Background
{
    internal class QueueHostedTaskService : BackgroundService
    {
        private readonly IBackgroundTaskQueue<Func<CancellationToken, ValueTask>> _taskQueue;
        private readonly ILogger<QueueHostedTaskService> _logger;

        public QueueHostedTaskService(
            IBackgroundTaskQueue<Func<CancellationToken, ValueTask>> taskQueue,
            ILogger<QueueHostedTaskService> logger) =>
            (_taskQueue, _logger) = (taskQueue, logger);

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information(
                $"{nameof(QueueHostedTaskService)} is running.{Environment.NewLine}" +
                $"{Environment.NewLine}Tap W to add a work item to the " +
                $"background queue.{Environment.NewLine}");
            return Task.WhenAll( ProcessTaskQueueAsync(stoppingToken),test(stoppingToken));
        }
        private async Task test(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(10000, stoppingToken);
                    Log.Information("QueueHostedTaskService test");
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
        private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Func<CancellationToken, ValueTask>? workItem = await _taskQueue.DequeueAsync(stoppingToken);

                    await workItem(stoppingToken);
                    Log.Information("QueueHostedTaskService");
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
            Log.Information($"{nameof(QueueHostedTaskService)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
