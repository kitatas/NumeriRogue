using System;
using R3;

namespace PrimeMillionaire.Boot.Domain.UseCase
{
    public sealed class StateUseCase : IDisposable
    {
        private readonly Subject<BootState> _state;

        public StateUseCase()
        {
            _state = new Subject<BootState>();
        }

        public Observable<BootState> state => _state.Where(x => x != BootState.None);

        public void Set(BootState value) => _state?.OnNext(value);

        public void Dispose()
        {
            _state?.Dispose();
        }
    }
}