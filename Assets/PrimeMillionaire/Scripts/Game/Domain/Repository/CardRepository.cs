using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Game.Data.DataStore;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class CardRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public CardRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public IEnumerable<CardVO> GetAll()
        {
            return _memoryDatabase.CardMasterTable.All
                .Select(x => x.card);
        }
    }
}