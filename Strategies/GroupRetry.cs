using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.Retryable.Strategies
{
    public sealed class GroupRetry : IRetryStrategy
    {
        public GroupRetry(params IRetryStrategy[] retryStrategies)
        {
            _retryStrategies = new List<IRetryStrategy>(retryStrategies);
        }

        public async Task<Boolean> PrepareToRetry(Exception exception, CancellationToken cancellationToken)
        {
            using (List<IRetryStrategy>.Enumerator strategiesEnumerator = _retryStrategies.GetEnumerator())
            {
                Boolean canRetry = true;
                while (canRetry && strategiesEnumerator.MoveNext())
                {
                    IRetryStrategy strategy = strategiesEnumerator.Current;
                    canRetry = await strategy.PrepareToRetry(exception, cancellationToken);
                }
                return canRetry;
            }
        }

        private readonly List<IRetryStrategy> _retryStrategies;
    }
}
