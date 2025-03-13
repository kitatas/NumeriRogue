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
        private readonly InterruptUseCase _interruptUseCase;
        private readonly ModalUseCase _modalUseCase;
        private readonly InterruptView _interruptView;

        public InterruptState(InterruptUseCase interruptUseCase, ModalUseCase modalUseCase, InterruptView interruptView)
        {
            _interruptUseCase = interruptUseCase;
            _modalUseCase = modalUseCase;
            _interruptView = interruptView;
        }

        public override BootState state => BootState.Interrupt;

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            await _modalUseCase.ShowAsync(ModalType.Interrupt, token);

            var action = await _interruptView.PushAnyAsync(token);
            switch (action)
            {
                case ButtonType.Decision:
                    return BootState.Restart;
                case ButtonType.Cancel:
                    _interruptUseCase.Delete();
                    return BootState.Load;
                default:
                    throw new Exception();
            }
        }
    }
}