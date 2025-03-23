using System;
using PrimeMillionaire.Common.Data.Entity;
using R3;

namespace PrimeMillionaire.Top.Domain.UseCase
{
    public sealed class StateUseCase : IDisposable
    {
        private readonly RetryCountEntity _retryCountEntity;
        private readonly BehaviorSubject<TopState> _state;

        public StateUseCase(RetryCountEntity retryCountEntity)
        {
            _retryCountEntity = retryCountEntity;
            _state = new BehaviorSubject<TopState>(TopState.None);
        }

        public Observable<TopState> state => _state.Where(x => x != TopState.None);

        public void Set(TopState value) => _state?.OnNext(value);

        public bool IsMaxRetry(TopState value)
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