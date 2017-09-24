using System;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.Retryable.Strategies
{
    public sealed class DelayRetry : IRetryStrategy
    {
        public DelayRetry(TimeSpan delay)
        {
            Delay = delay;
        }

        public TimeSpan Delay { get; }

        public async Task<Boolean> PrepareToRetry(Exception exception, CancellationToken cancellationToken)
        {
            await Task.Delay(Delay, cancellationToken);
            return true;
        }
    }
}
