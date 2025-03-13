using System.Threading;
using Cysharp.Threading.Tasks;
using VitalRouter;

namespace PrimeMillionaire.Top.Domain.UseCase
{
    public sealed class ModalUseCase
    {
        public async UniTask ShowAsync(ModalType type, CancellationToken token)
        {
            await PopupAsync(type, true, token);
        }

        public async UniTask HideAsync(ModalType type, CancellationToken token)
        {
            await PopupAsync(type, false, token);
        }

        public async UniTask PopupAsync(ModalType type, bool isActive, CancellationToken token)
        {
            await Router.Default.PublishAsync(new ModalVO(type, isActive), token);
        }
    }
}