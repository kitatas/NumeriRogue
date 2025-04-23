using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Boot.Presentation.View;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Utility;
using R3;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class LoginState : BaseState
    {
        private readonly InterruptUseCase _interruptUseCase;
        private readonly LoadingUseCase _loadingUseCase;
        private readonly LoginUseCase _loginUseCase;
        private readonly TitleView _titleView;

        public LoginState(InterruptUseCase interruptUseCase, LoadingUseCase loadingUseCase, LoginUseCase loginUseCase,
            TitleView titleView)
        {
            _interruptUseCase = interruptUseCase;
            _loadingUseCase = loadingUseCase;
            _loginUseCase = loginUseCase;
            _titleView = titleView;
        }

        public override BootState state => BootState.Login;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _titleView.Init();
            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            // 他presenter初期化待ち
            await UniTaskHelper.DelayAsync(0.5f, token);
            _loadingUseCase.Set(false);

            var isSuccess = _loginUseCase.Login();
            if (isSuccess == false)
            {
                throw new RebootExceptionVO(ExceptionConfig.FAILED_LOGIN);
            }

            var hasInterrupt = _interruptUseCase.HasInterrupt();
            if (hasInterrupt)
            {
                return BootState.Interrupt;
            }

            await _titleView.push.FirstAsync(cancellationToken: token);
            return BootState.Load;
        }
    }
}