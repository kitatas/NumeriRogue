using System;
using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Common.Data.DataStore;
using PrimeMillionaire.Common.Utility;
using UniEx;

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
            var master = _memoryDatabase.CardMasterTable.All;
            var cards = type switch
            {
                // TODO: ベタ書き
                CharacterType.Andromeda => master.Where(x => x.Suit.ToSuit() is Suit.Spade or Suit.Heart),
                CharacterType.Borealjuggernaut => master.Where(x => !x.Rank.IsEven()),
                CharacterType.Dissonance => master.Where(x => x.Rank >= 7),
                CharacterType.Kron => master.Where(x => x.Rank <= 7),
                CharacterType.Paragon => master,
                _ => throw new Exception(),
            };

            return cards.Select(x => x.ToVO());
        }
    }
}