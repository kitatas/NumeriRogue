using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class LoginState : BaseState
    {
        private readonly LoginUseCase _loginUseCase;

        public LoginState(LoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        public override BootState state => BootState.Login;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            var (isSuccess, hasInterrupt) = _loginUseCase.Login();
            if (isSuccess == false)
            {
                throw new Exception();
            }

            if (hasInterrupt)
            {
                return BootState.Interrupt;
            }

            await UniTask.Yield(token);
            return BootState.Load;
        }
    }
}