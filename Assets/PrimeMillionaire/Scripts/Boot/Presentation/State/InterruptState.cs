using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class InterruptState : BaseState
    {
        private readonly InterruptUseCase _interruptUseCase;
        private readonly SceneUseCase _sceneUseCase;
        private readonly ModalUseCase _modalUseCase;

        public InterruptState(InterruptUseCase interruptUseCase, SceneUseCase sceneUseCase, ModalUseCase modalUseCase)
        {
            _interruptUseCase = interruptUseCase;
            _sceneUseCase = sceneUseCase;
            _modalUseCase = modalUseCase;
        }

        public override BootState state => BootState.Interrupt;

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            await _modalUseCase.ShowAsync(ModalType.Interrupt, token);

            var type = await _interruptUseCase.PressButtonAsync(token);
            var sceneName = type switch
            {
                ButtonType.Decision => SceneName.Game,
                ButtonType.Cancel => SceneName.Top,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BUTTON),
            };
            _sceneUseCase.Load(sceneName, LoadType.Fade);

            return BootState.None;
        }
    }
}