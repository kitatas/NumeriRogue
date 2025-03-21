using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Game.Domain.UseCase;
using PrimeMillionaire.Game.Presentation.State;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Game.Presentation.Presenter
{
    public sealed class StatePresenter : IAsyncStartable
    {
        private readonly ExceptionUseCase _exceptionUseCase;
        private readonly StateUseCase _stateUseCase;
        private readonly List<BaseState> _states;

        public StatePresenter(ExceptionUseCase exceptionUseCase, StateUseCase stateUseCase,
            IEnumerable<BaseState> states)
        {
            _exceptionUseCase = exceptionUseCase;
            _stateUseCase = stateUseCase;
            _states = states.ToList();
        }

        public async UniTask StartAsync(CancellationToken token)
        {
            await UniTask.WhenAll(_states
                .Select(x => x.InitAsync(token)));

            _stateUseCase.state
                .Subscribe(x => ExecAsync(x, token).Forget())
                .AddTo(token);

            _stateUseCase.Init();
        }

        private async UniTaskVoid ExecAsync(GameState state, CancellationToken token)
        {
            try
            {
                var currentState = _states.Find(x => x.state == state);
                if (currentState == null)
                {
                    throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_STATE);
                }

                var nextState = await currentState.TickAsync(token);
                _stateUseCase.Set(nextState);
            }
            catch (ExceptionVO e)
            {
                await _exceptionUseCase.ThrowAsync(e, token);
                if (e is RetryExceptionVO)
                {
                    _stateUseCase.Set(state);
                }
            }
            catch (Exception e)
            {
                await _exceptionUseCase.ThrowQuitAsync(e.Message, token);
            }
        }
    }
}