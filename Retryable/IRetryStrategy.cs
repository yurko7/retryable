using System;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.Retryable
{
    public interface IRetryStrategy
    {
        Task<Boolean> PrepareToRetry(Exception exception, CancellationToken cancellationToken);
    }
}