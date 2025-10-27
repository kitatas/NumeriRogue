using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class LoginState : BaseState
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly LoginUseCase _loginUseCase;

        public LoginState(LoadingUseCase loadingUseCase, LoginUseCase loginUseCase)
        {
            _loadingUseCase = loadingUseCase;
            _loginUseCase = loginUseCase;
        }

        public override BootState state => BootState.Login;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            _loadingUseCase.Set(true);

            var isSuccess = await _loginUseCase.LoginAsync(token);

            _loadingUseCase.Set(false);

            if (isSuccess == false)
            {
                throw new RebootExceptionVO(ExceptionConfig.FAILED_LOGIN);
            }

            return BootState.Check;
        }
    }
}