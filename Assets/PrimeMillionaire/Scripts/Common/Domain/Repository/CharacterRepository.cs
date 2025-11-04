using System.Linq;
using FastEnumUtility;
using PrimeMillionaire.Common.Data.DataStore;
using UniEx;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class CharacterRepository
    {
        private readonly MemoryDbData _memoryDbData;

        public CharacterRepository(MemoryDbData memoryDbData)
        {
            _memoryDbData = memoryDbData;
        }

        public CharacterVO FindOther(CharacterType type)
        {
            return _memoryDbData.Get().CharacterMasterTable.All
                .Where(x => x.Type.ToCharacterType() != type)
                .ToList()
                .GetRandom()
                .ToVO();
        }

        public CharacterVO Find(CharacterType type)
        {
            if (_memoryDbData.Get().CharacterMasterTable.TryFindByType(type.ToInt32(), out var master))
            {
                return master.ToVO();
            }
            else
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_CHARACTER);
            }
        }
    }
}