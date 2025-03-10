using System.Threading;
using Cysharp.Threading.Tasks;
using VitalRouter;

namespace PrimeMillionaire.Top.Domain.UseCase
{
    public sealed class ModalUseCase
    {
        public async UniTask PopupAsync(ModalType type, CancellationToken token)
        {
            await Router.Default.PublishAsync(new ModalVO(type, true), token);
        }
    }
}