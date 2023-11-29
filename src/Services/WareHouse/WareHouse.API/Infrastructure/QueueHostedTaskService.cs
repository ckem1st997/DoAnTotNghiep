//#nullable enable
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using Serilog;
//using Share.Base.Core.StackQueue;
//using ShareModels.Models;
//using StackExchange.Profiling.Internal;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace WareHouse.API.Infrastructure
//{
//    /// <summary>
//    /// sau các BackgroundService sẽ viết tại service quản lý 
//    /// </summary>
//    internal class QueueHostedTaskService : BackgroundService
//    {
//        private readonly IBackgroundTaskQueue<Func<CancellationToken, ValueTask>> _taskQueue;
//        private readonly IBackgroundTaskQueue<UpdateViewer> _queue;
//        private readonly ILogger<QueueHostedTaskService> _logger;
//        private readonly IRepositoryEF<Domain.Entity.Inward> _repository;
//        private readonly IRepositoryEF<Domain.Entity.Outward> _repositoryOut;

//        private int ViewerBefore;
//        private float CountUpdate = 0.3f;
//        public QueueHostedTaskService(IBackgroundTaskQueue<Func<CancellationToken, ValueTask>> taskQueue, IBackgroundTaskQueue<UpdateViewer> queue, ILogger<QueueHostedTaskService> logger, IRepositoryEF<Domain.Entity.Inward> repository, IRepositoryEF<Domain.Entity.Outward> repositoryOut)
//        {
//            _taskQueue = taskQueue;
//            _queue = queue;
//            _logger = logger;
//            ViewerBefore = 0;
//            _repository = repository;
//            _repositoryOut = repositoryOut;
//        }




//        protected override Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            Log.Information(
//                $"{nameof(QueueHostedTaskService)} is running.{Environment.NewLine}" +
//                $"{Environment.NewLine}Tap W to add a work item to the " +
//                $"background queue.{Environment.NewLine}");
//            return Task.WhenAll(ProcessTaskQueueAsync(stoppingToken), UpdateViewer(stoppingToken));
//        }
//        private async Task UpdateViewer(CancellationToken stoppingToken)
//        {
//            if (!stoppingToken.IsCancellationRequested)
//                Log.Information("!stoppingToken.IsCancellationRequested : ");
//            else
//                Log.Information("stoppingToken.IsCancellationRequested : ");
//            IDictionary<string, int> numberNames = new Dictionary<string, int>();
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                try
//                {
//                    UpdateViewer? workItem = await _queue.DequeueAsync(stoppingToken);
//                    if (workItem != null)
//                    {
//                        if (numberNames != null && numberNames.ContainsKey(workItem.Id + workItem.TypeWareHouse))
//                            numberNames[workItem.Id + workItem.TypeWareHouse]++;
//                        else
//                            numberNames?.Add(workItem.Id + workItem.TypeWareHouse, 1);

//                    }
//                    ViewerBefore++;
//                    int count = await _queue.CountqueueAsync();
//                    if (count == 0)
//                    {
//                        Log.Information("QueueHostedTaskService test : " + ViewerBefore);
//                        if (numberNames != null && numberNames.Count > 0)
//                        {
//                            foreach (var item in numberNames)
//                            {
//                                if (item.Key.Contains(WareHouseBookEnum.Inward.ToString()))
//                                {
//                                    var model = await _repository.GetByIdsync(item.Key.Replace(WareHouseBookEnum.Inward.ToString(),string.Empty));
//                                    if (model is not null)
//                                    {
//                                        int view = (int)(model.Viewer ?? 0);
//                                        model.Viewer = view + item.Value;
//                                    }
//                                }
//                                else
//                                {
//                                    var model = await _repositoryOut.GetByIdsync(item.Key.Replace(WareHouseBookEnum.Outward.ToString(), string.Empty));
//                                    if (model is not null)
//                                    {
//                                        int view = (int)(model.Viewer ?? 0);
//                                        model.Viewer = view + item.Value;
//                                    }
//                                }
                                
//                            }
//                            var res = await _repository.SaveChangesConfigureAwaitAsync(cancellationToken: stoppingToken);
//                            numberNames.Clear();
//                            ViewerBefore = 0;
//                        }

//                    }



//                    //   await Task.Delay(1, stoppingToken);

//                }
//                catch (OperationCanceledException)
//                {
//                    // Prevent throwing if stoppingToken was signaled
//                }
//                catch (Exception ex)
//                {
//                    Log.Error(ex, "Error occurred executing task work item");
//                }
//            }
//        }
//        private async Task ProcessTaskQueueAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                try
//                {
//                    Func<CancellationToken, ValueTask>? workItem = await _taskQueue.DequeueAsync(stoppingToken);

//                    await workItem(stoppingToken);
//                    Log.Information("QueueHostedTaskService");
//                }
//                catch (OperationCanceledException)
//                {
//                    // Prevent throwing if stoppingToken was signaled
//                }
//                catch (Exception ex)
//                {
//                    Log.Error(ex, "Error occurred executing task work item.");
//                }
//            }
//        }

//        public override async Task StopAsync(CancellationToken stoppingToken)
//        {
//            Log.Information($"{nameof(QueueHostedTaskService)} is stopping.");

//            await base.StopAsync(stoppingToken);
//        }
//    }
//}
