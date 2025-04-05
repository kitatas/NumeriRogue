using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Data.DataStore;
using UnityEngine;

namespace PrimeMillionaire.Game.Domain.Repository
{
    public sealed class BuffRepository
    {
        private readonly BuffTable _buffTable;

        public BuffRepository(BuffTable buffTable)
        {
            _buffTable = buffTable;
        }

        public GameObject FindFxObject(SkillType type)
        {
            var data = _buffTable.list.Find(x => x.type == type);
            if (data == null)
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_FX);
            }

            return data.fx;
        }
    }
}