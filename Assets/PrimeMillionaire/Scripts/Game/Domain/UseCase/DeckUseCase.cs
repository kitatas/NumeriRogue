using PrimeMillionaire.Game.Data.Entity;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class DeckUseCase
    {
        private readonly DeckEntity _deckEntity;

        public DeckUseCase(DeckEntity deckEntity)
        {
            _deckEntity = deckEntity;
        }

        public void Build()
        {
            _deckEntity.Shuffle();
        }
    }
}