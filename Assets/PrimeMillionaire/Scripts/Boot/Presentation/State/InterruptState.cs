using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Boot.Presentation.View;
using PrimeMillionaire.Common;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class InterruptState : BaseState
    {
        private readonly ModalUseCase _modalUseCase;
        private readonly InterruptView _interruptView;

        public InterruptState(ModalUseCase modalUseCase, InterruptView interruptView)
        {
            _modalUseCase = modalUseCase;
            _interruptView = interruptView;
        }

        public override BootState state => BootState.Interrupt;

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            await _modalUseCase.PopupAsync(ModalType.Interrupt, token);

            var action = await _interruptView.PushAnyAsync(token);
            switch (action)
            {
                case ButtonType.Decision:
                    return BootState.Restart;
                case ButtonType.Cancel:
                    return BootState.Load;
                default:
                    throw new Exception();
            }
        }
    }
}