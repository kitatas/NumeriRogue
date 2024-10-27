using R3;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class StateUseCase
    {
        private readonly ReactiveProperty<GameState> _state;

        public StateUseCase()
        {
            _state = new ReactiveProperty<GameState>(GameConfig.INIT_STATE);
        }

        public Observable<GameState> state => _state.Where(x => x != GameState.None);

        public void Set(GameState value) => _state.Value = value;
    }
}