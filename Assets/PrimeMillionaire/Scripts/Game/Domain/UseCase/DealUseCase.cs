using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class DealUseCase
    {
        private readonly DeckEntity _deckEntity;
        private readonly CardRepository _cardRepository;

        public DealUseCase(DeckEntity deckEntity, CardRepository cardRepository)
        {
            _deckEntity = deckEntity;
            _cardRepository = cardRepository;
        }

        public void Init()
        {
            _deckEntity.Init(_cardRepository.GetAll());
        }
    }
}