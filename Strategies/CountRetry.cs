using System;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.Retryable.Strategies
{
    public sealed class CountRetry : IRetryStrategy
    {
        public CountRetry(Int32 maxTries)
        {
            MaxTries = maxTries;
        }

        public Int32 MaxTries { get; }

        public Int32 Tries { get; private set; }

        public Task<Boolean> PrepareToRetry(Exception exception, CancellationToken cancellationToken)
        {
            // Keep counting each try even if their number exceeds MaxTries.
            Boolean canRetry = Tries++ < MaxTries;
            return Task.FromResult(canRetry);
        }
    }
}
