using System;
using R3;

namespace PrimeMillionaire.Top.Domain.UseCase
{
    public sealed class StateUseCase : IDisposable
    {
        private readonly Subject<TopState> _state;

        public StateUseCase()
        {
            _state = new Subject<TopState>();
        }

        public Observable<TopState> state => _state.Where(x => x != TopState.None);

        public void Set(TopState value) => _state?.OnNext(value);

        public void Dispose()
        {
            _state?.Dispose();
        }
    }
}