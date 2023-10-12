using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Share.Base.Core.StackQueue
{
    public interface IBackgroundTaskQueue<T>
    {
        ValueTask QueueBackgroundWorkItemAsync(T workItem);

        ValueTask<T> DequeueAsync(CancellationToken cancellationToken);
        ValueTask<int> CountqueueAsync();
    }

    public interface IBackgroundTaskStack<T>
    {
        Task StackBackgroundWorkItem(T workItem, CancellationToken cancellationToken = default);

        Task<T> Dequeue(CancellationToken cancellationToken);
        Task<bool> CheckStack();
        Task<int> COUNT();

    }


    /// <summary>
    ///  valuetask dùng để trả về một giá trị đồng bộ nhưng muốn sử dụng cơ chết bất đồng bo 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DefaultBackgroundTaskQueue<T> : IBackgroundTaskQueue<T>
    {
        private readonly Channel<T> _queue;

        public DefaultBackgroundTaskQueue()
        {
            BoundedChannelOptions options = new(10000)
            {
                FullMode = BoundedChannelFullMode.Wait
            };
            _queue = Channel.CreateBounded<T>(options);
        }

        public async ValueTask QueueBackgroundWorkItemAsync(T workItem)
        {
            if (workItem is null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            await _queue.Writer.WriteAsync(workItem);
        }

        public async ValueTask<T> DequeueAsync(
            CancellationToken cancellationToken)
        {
            T workItem = await _queue.Reader.ReadAsync(cancellationToken);

            return workItem;
        }

        public async ValueTask<int> CountqueueAsync()
        {
            int workItem = _queue.Reader.Count;

            return await ValueTask.FromResult(workItem);
        }
    }


    public sealed class DefaultBackgroundTaskStack<T> : IBackgroundTaskStack<T>
    {
        private readonly Stack<T> _queue;

        public DefaultBackgroundTaskStack()
        {
            _queue = new Stack<T>();
        }

        public async Task StackBackgroundWorkItem(T workItem, CancellationToken cancellationToken = default)
        {
            if (workItem is null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }
            if (cancellationToken.IsCancellationRequested)
            {
                await Task.FromCanceled<T>(cancellationToken);
            }
            try
            {
                _queue.Push(workItem);
                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                await Task.FromException(e);
            }
        }

        public async Task<T> Dequeue(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return await Task.FromCanceled<T>(cancellationToken);
            }

            try
            {
                if (_queue.TryPop(out T fastItem))
                {
                    return await Task.FromResult(fastItem);
                }
            }
            catch (Exception exc) when (exc is OperationCanceledException)
            {
                return await Task.FromException<T>(exc);
            }
            return await Task.FromCanceled<T>(cancellationToken);

        }

        public async Task<bool> CheckStack()
        {
            return await Task.FromResult(_queue.Count > 0);

        }
        public async Task<int> COUNT()
        {
            return await Task.FromResult(_queue.Count);
        }

    }

}
