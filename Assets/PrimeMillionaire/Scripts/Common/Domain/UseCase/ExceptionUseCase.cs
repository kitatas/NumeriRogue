using System.Threading;
using Cysharp.Threading.Tasks;
using VitalRouter;

namespace PrimeMillionaire.Common.Domain.UseCase
{
    public sealed class ExceptionUseCase
    {
        public async UniTask ThrowRebootAsync(string message, CancellationToken token)
        {
            await Router.Default.PublishAsync(new RebootExceptionVO(message), token);
        }

        public async UniTask ThrowRetryAsync(string message, CancellationToken token)
        {
            await Router.Default.PublishAsync(new RetryExceptionVO(message), token);
        }

        public async UniTask ThrowQuitAsync(string message, CancellationToken token)
        {
            await Router.Default.PublishAsync(new QuitExceptionVO(message), token);
        }
    }
}