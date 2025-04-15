using System.Collections.Generic;
using System.Linq;
using FastEnumUtility;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class CharacterStageRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public CharacterStageRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public IEnumerable<CardVO> GetCards(CharacterType type)
        {
            if (_memoryDatabase.CharacterStageMasterTable.TryFindByType(type.ToInt32(), out var characterStage))
            {
                return _memoryDatabase.CardMasterTable.All
                    .Where(x => characterStage.Suits.Contains(x.Suit) && characterStage.Ranks.Contains(x.Rank))
                    .Select(x => x.ToVO());
            }
            else
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_CARD);
            }
        }
    }
}