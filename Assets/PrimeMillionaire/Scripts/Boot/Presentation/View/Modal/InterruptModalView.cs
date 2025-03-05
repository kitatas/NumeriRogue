using System.Threading;
using Cysharp.Threading.Tasks;

namespace PrimeMillionaire.Boot.Presentation.View.Modal
{
    public sealed class InterruptModalView : BaseModalView
    {
        public override ModalType type => ModalType.Interrupt;

        public override async UniTask PopupAsync(float duration, CancellationToken token)
        {
            await Show(duration).WithCancellation(token);
        }
    }
}