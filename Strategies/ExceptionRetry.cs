using System;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.Retryable.Strategies
{
    public sealed class ExceptionRetry : IRetryStrategy
    {
        public ExceptionRetry(Func<Exception, Boolean> predicate)
        {
            _predicate = predicate;
        }

        public Task<Boolean> PrepareToRetry(Exception exception, CancellationToken cancellationToken)
        {
            Boolean canRetry = _predicate(exception);
            return Task.FromResult(canRetry);
        }

        private readonly Func<Exception, Boolean> _predicate;
    }
}
