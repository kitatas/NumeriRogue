using System;
using PrimeMillionaire.Common.Domain.Repository;
using R3;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class StateUseCase : IDisposable
    {
        private readonly SaveRepository _saveRepository;
        private readonly Subject<GameState> _state;

        public StateUseCase(SaveRepository saveRepository)
        {
            _saveRepository = saveRepository;
            _state = new Subject<GameState>();
        }

        public Observable<GameState> state => _state.Where(x => x != GameState.None);

        public void Set(GameState value) => _state?.OnNext(value);

        public void Init()
        {
            var loadState = _saveRepository.TryLoadInterrupt(out _) ? GameState.Restart : GameState.Init;
            Set(loadState);
        }

        public void Dispose()
        {
            _state?.Dispose();
        }
    }
}