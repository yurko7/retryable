using System;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.Retryable.Strategies
{
    public sealed class BackoffRetry : IRetryStrategy
    {
        public Int32 Tries { get; private set; }

        public async Task<Boolean> PrepareToRetry(Exception exception, CancellationToken cancellationToken)
        {
            Int32 exponent = Tries++ % 31;
            Int32 delay = (1 << exponent) * 1000;
            Int32 maxDeviation = delay / 5;
            Int32 deviation = _random.Next(delay - maxDeviation, delay + maxDeviation);
            await Task.Delay(delay + deviation, cancellationToken);
            return true;
        }

        private readonly Random _random = new Random();
    }
}
