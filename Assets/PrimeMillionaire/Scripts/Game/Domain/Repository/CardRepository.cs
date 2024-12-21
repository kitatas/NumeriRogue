using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Game.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class CardRepository
    {
        private readonly CardTable _cardTable;

        public CardRepository(CardTable cardTable)
        {
            _cardTable = cardTable;
        }

        public IEnumerable<CardVO> GetAll()
        {
            return _cardTable.records.Select(x => x.card);
        }
    }
}