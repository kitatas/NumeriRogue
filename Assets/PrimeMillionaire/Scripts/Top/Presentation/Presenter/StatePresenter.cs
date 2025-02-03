using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Top.Domain.UseCase;
using PrimeMillionaire.Top.Presentation.State;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Top.Presentation.Presenter
{
    public sealed class StatePresenter : IAsyncStartable
    {
        private readonly StateUseCase _stateUseCase;
        private readonly List<BaseState> _states;

        public StatePresenter(StateUseCase stateUseCase, IEnumerable<BaseState> states)
        {
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

            _stateUseCase.Set(TopConfig.INIT_STATE);
        }

        private async UniTaskVoid ExecAsync(TopState state, CancellationToken token)
        {
            try
            {
                var currentState = _states.Find(x => x.state == state);
                if (currentState == null)
                {
                    // TODO: Exception
                    throw new Exception();
                }

                var nextState = await currentState.TickAsync(token);
                _stateUseCase.Set(nextState);
            }
            catch (Exception e)
            {
                // TODO: retry
                throw;
            }
        }
    }
}