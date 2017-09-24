using System;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.Retryable
{
    public static class RetryableTask
    {
        public static async Task Retry(this Func<Task> asyncFunc, IRetryStrategy strategy, CancellationToken cancellationToken = default(CancellationToken))
        {
            do
            {
                Exception exception;
                try
                {
                    await asyncFunc();
                    return;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                Boolean canRetry = await strategy.PrepareToRetry(exception, cancellationToken);
                if (!canRetry || cancellationToken.IsCancellationRequested)
                {
                    throw exception;
                }
            }
            while (true);
        }

        public static async Task<T> Retry<T>(this Func<Task<T>> asyncFunc, IRetryStrategy strategy, CancellationToken cancellationToken = default(CancellationToken))
        {
            do
            {
                Exception exception;
                try
                {
                    T result = await asyncFunc();
                    return result;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                Boolean canRetry = await strategy.PrepareToRetry(exception, cancellationToken);
                if (!canRetry || cancellationToken.IsCancellationRequested)
                {
                    throw exception;
                }
            }
            while (true);
        }
    }
}