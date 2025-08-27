using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Common;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class InterruptState : BaseState
    {
        private readonly InterruptUseCase _interruptUseCase;
        private readonly ModalUseCase _modalUseCase;

        public InterruptState(InterruptUseCase interruptUseCase, ModalUseCase modalUseCase)
        {
            _interruptUseCase = interruptUseCase;
            _modalUseCase = modalUseCase;
        }

        public override BootState state => BootState.Interrupt;

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            await _modalUseCase.ShowAsync(ModalType.Interrupt, token);

            var type = await _interruptUseCase.PressButtonAsync(token);
            return type switch
            {
                ButtonType.Decision => BootState.Restart,
                ButtonType.Cancel => BootState.Load,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BUTTON),
            };
        }
    }
}