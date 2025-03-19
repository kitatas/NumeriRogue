using System.Collections.Generic;
using System.Linq;
using FastEnumUtility;
using PrimeMillionaire.Common.Data.DataStore;
using PrimeMillionaire.Common.Utility;
using UniEx;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class CharacterRepository
    {
        private readonly MemoryDatabase _memoryDatabase;

        public CharacterRepository(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public List<CharacterVO> GetAll()
        {
            return _memoryDatabase.CharacterMasterTable.All
                .Select(x => x.ToVO())
                .ToList();
        }

        public List<CharacterVO> GetUpto(int characterNo)
        {
            return _memoryDatabase.CharacterMasterTable.All
                .Where(x => x.Type <= characterNo)
                .Select(x => x.ToVO())
                .ToList();
        }

        public List<CharacterVO> GetReleased(ProgressVO progress)
        {
            var released = progress.clears
                .Where(x => x.isClear)
                .Select(x => x.characterNo);

            return _memoryDatabase.CharacterMasterTable.All
                .Where(x => x.ReleaseConditions.All(y => released.Contains(y)))
                .Select(x => x.ToVO())
                .ToList();
        }

        public CharacterVO FindOther(CharacterType type)
        {
            return _memoryDatabase.CharacterMasterTable.All
                .Where(x => x.Type.ToCharacterType() != type)
                .ToList()
                .GetRandom()
                .ToVO();
        }

        public CharacterVO Find(CharacterType type)
        {
            if (_memoryDatabase.CharacterMasterTable.TryFindByType(type.ToInt32(), out var master))
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