using System.Collections.Generic;
using System.Linq;
using FastEnumUtility;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class DeckRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public DeckRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public IEnumerable<CardVO> GetCards(CharacterType type)
        {
            if (_memoryDatabase.DeckMasterTable.TryFindByType(type.ToInt32(), out var deck))
            {
                return _memoryDatabase.CardMasterTable.All
                    .Where(x => deck.Suits.Contains(x.Suit) && deck.Ranks.Contains(x.Rank))
                    .Select(x => x.ToVO());
            }
            else
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_CARD);
            }
        }
    }
}