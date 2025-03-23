using System;
using PrimeMillionaire.Common.Data.Entity;
using R3;

namespace PrimeMillionaire.Boot.Domain.UseCase
{
    public sealed class StateUseCase : IDisposable
    {
        private readonly RetryCountEntity _retryCountEntity;
        private readonly BehaviorSubject<BootState> _state;

        public StateUseCase(RetryCountEntity retryCountEntity)
        {
            _retryCountEntity = retryCountEntity;
            _state = new BehaviorSubject<BootState>(BootState.None);
        }

        public Observable<BootState> state => _state.Where(x => x != BootState.None);

        public void Set(BootState value) => _state?.OnNext(value);

        public bool IsMaxRetry(BootState value)
        {
            _retryCountEntity.Update(_state.Value == value);
            return _retryCountEntity.IsMaxRetry();
        }

        public void Dispose()
        {
            _state?.Dispose();
        }
    }
}