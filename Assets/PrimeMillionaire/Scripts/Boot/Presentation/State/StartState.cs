using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class StartState : BaseState
    {
        private readonly InterruptUseCase _interruptUseCase;
        private readonly SceneUseCase _sceneUseCase;
        private readonly ISoundUseCase _soundUseCase;
        private readonly TitleUseCase _titleUseCase;

        public StartState(InterruptUseCase interruptUseCase, SceneUseCase sceneUseCase, ISoundUseCase soundUseCase,
            TitleUseCase titleUseCase)
        {
            _interruptUseCase = interruptUseCase;
            _sceneUseCase = sceneUseCase;
            _soundUseCase = soundUseCase;
            _titleUseCase = titleUseCase;
        }

        public override BootState state => BootState.Start;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            _soundUseCase.Play(Bgm.Menu);

            var hasInterrupt = _interruptUseCase.HasInterrupt();
            if (hasInterrupt)
            {
                return BootState.Interrupt;
            }

            await _titleUseCase.TouchScreenAsync(token);

            _sceneUseCase.Load(SceneName.Top, LoadType.Fade);
            return BootState.None;
        }
    }
}