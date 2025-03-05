using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Boot.Presentation.View;
using PrimeMillionaire.Common.Utility;
using R3;

namespace PrimeMillionaire.Boot.Presentation.State
{
    public sealed class LoginState : BaseState
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly TitleView _titleView;

        public LoginState(LoginUseCase loginUseCase, TitleView titleView)
        {
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

            var (isSuccess, hasInterrupt) = _loginUseCase.Login();
            if (isSuccess == false)
            {
                throw new Exception();
            }

            if (hasInterrupt)
            {
                return BootState.Interrupt;
            }

            await _titleView.push.FirstAsync(cancellationToken: token);
            return BootState.Load;
        }
    }
}