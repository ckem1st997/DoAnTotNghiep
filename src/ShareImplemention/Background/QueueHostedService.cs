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
    /// <summary>
    /// tạo riêng một QueueHosted để xử lý notication
    /// thay vì phải đợi xem có gửi được thông báo đi hay không
    /// thì phần thông báo sẽ xử lý nền
    /// vì giúp trả về response cho client nhanh hơn, tránh đợi
    /// và giúp ngắn gọn code xử lý ở một nơi thay vì nhiều nơi
    /// </summary>
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
            stoppingToken.Register(() => Log.Debug("#1 GracePeriodManagerService background task is stopping."));

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
