using PrimeMillionaire.Game.Data.Entity;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class HandUseCase
    {
        private readonly DeckEntity _deckEntity;

        public HandUseCase(DeckEntity deckEntity)
        {
            _deckEntity = deckEntity;
        }

        public void SetUp()
        {
            _deckEntity.SetUp();
        }
    }
}