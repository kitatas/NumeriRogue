using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Boot.Domain.UseCase;
using PrimeMillionaire.Boot.Presentation.State;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Boot.Presentation.Presenter
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

            _stateUseCase.Set(BootConfig.INIT_STATE);
        }

        private async UniTaskVoid ExecAsync(BootState state, CancellationToken token)
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
                    ExecAsync(state, token).Forget();
                }

                throw;
            }
            catch (Exception e)
            {
                await _exceptionUseCase.ThrowQuitAsync(e.Message, token);
                throw;
            }
        }
    }
}