using System.Threading;
using Cysharp.Threading.Tasks;
using VitalRouter;

namespace PrimeMillionaire.Common.Domain.UseCase
{
    public sealed class ExceptionUseCase
    {
        public async UniTask ThrowAsync(ExceptionVO exception, CancellationToken token)
        {
            await Router.Default.PublishAsync(exception, token);
        }

        public async UniTask ThrowRebootAsync(string message, CancellationToken token)
        {
            await ThrowAsync(new RebootExceptionVO(message), token);
        }

        public async UniTask ThrowRetryAsync(string message, CancellationToken token)
        {
            await ThrowAsync(new RetryExceptionVO(message), token);
        }

        public async UniTask ThrowQuitAsync(string message, CancellationToken token)
        {
            await ThrowAsync(new QuitExceptionVO(message), token);
        }
    }
}