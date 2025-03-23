using System;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using R3;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class StateUseCase : IDisposable
    {
        private readonly RetryCountEntity _retryCountEntity;
        private readonly SaveRepository _saveRepository;
        private readonly BehaviorSubject<GameState> _state;

        public StateUseCase(RetryCountEntity retryCountEntity, SaveRepository saveRepository)
        {
            _retryCountEntity = retryCountEntity;
            _saveRepository = saveRepository;
            _state = new BehaviorSubject<GameState>(GameState.None);
        }

        public Observable<GameState> state => _state.Where(x => x != GameState.None);

        public void Set(GameState value) => _state?.OnNext(value);

        public void Init()
        {
            var loadState = _saveRepository.TryLoadInterrupt(out _) ? GameState.Restart : GameState.Init;
            Set(loadState);
        }

        public bool IsMaxRetry(GameState value)
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