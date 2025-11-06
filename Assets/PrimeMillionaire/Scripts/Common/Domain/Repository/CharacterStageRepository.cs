using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Common.Data.DataStore;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class CharacterStageRepository
    {
        private readonly MemoryDbData _memoryDbData;

        public CharacterStageRepository(MemoryDbData memoryDbData)
        {
            _memoryDbData = memoryDbData;
        }

        public DeckVO GetDeck(int id)
        {
            if (_memoryDbData.Get().CharacterStageMasterTable.TryFindById(id, out var characterStage))
            {
                var cards = _memoryDbData.Get().CardMasterTable.All
                    .Where(x => characterStage.Suits.Contains(x.Suit) && characterStage.Ranks.Contains(x.Rank))
                    .Select(x => x.ToVO());

                return new DeckVO(cards);
            }
            else
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_CARD);
            }
        }

        public IEnumerable<int> GetReleased(ProgressVO progress)
        {
            var released = progress.characterProgress
                .Where(x => x.isClear)
                .Select(x => x.id);

            return _memoryDbData.Get().CharacterStageMasterTable.All
                .Where(x => x.ReleaseConditions.All(y => released.Contains(y)))
                .Select(x => x.Id);
        }

        public StageVO GetStage(int id)
        {
            if (_memoryDbData.Get().CharacterStageMasterTable.TryFindById(id, out var master))
            {
                return master.ToStageVO();
            }
            else
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_STAGE);
            }
        }
    }
}