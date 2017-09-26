using System;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.Retryable
{
    public static class RetryableTask
    {
        public static async Task Retry(this Func<Task> asyncFunc, IRetryStrategy strategy, CancellationToken cancellationToken = default)
        {
            do
            {
                try
                {
                    await asyncFunc();
                    return;
                }
                catch (Exception ex)
                {
                    Boolean canRetry = await strategy.PrepareToRetry(ex, cancellationToken);
                    if (!canRetry || cancellationToken.IsCancellationRequested)
                    {
                        throw;
                    }
                }
            }
            while (true);
        }

        public static async Task<T> Retry<T>(this Func<Task<T>> asyncFunc, IRetryStrategy strategy, CancellationToken cancellationToken = default)
        {
            do
            {
                try
                {
                    T result = await asyncFunc();
                    return result;
                }
                catch (Exception ex)
                {
                    Boolean canRetry = await strategy.PrepareToRetry(ex, cancellationToken);
                    if (!canRetry || cancellationToken.IsCancellationRequested)
                    {
                        throw;
                    }
                }
            }
            while (true);
        }
    }
}