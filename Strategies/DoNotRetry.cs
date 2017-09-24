using System;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.Retryable.Strategies
{
    public sealed class DoNotRetry : IRetryStrategy
    {
        public Task<Boolean> PrepareToRetry(Exception exception, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
        }
    }
}
