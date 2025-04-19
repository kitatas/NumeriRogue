using PrimeMillionaire.Common.Data.DataStore;
using UnityEngine;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class SoundRepository
    {
        private readonly BgmTable _bgmTable;
        private readonly SeTable _seTable;

        public SoundRepository(BgmTable bgmTable, SeTable seTable)
        {
            _bgmTable = bgmTable;
            _seTable = seTable;
        }

        public AudioClip Find(Bgm bgm)
        {
            var data = _bgmTable.all.Find(x => x.bgm == bgm);
            if (data == null)
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BGM);
            }

            return data.clip;
        }

        public AudioClip Find(Se se)
        {
            var data = _seTable.all.Find(x => x.se == se);
            if (data == null)
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SE);
            }

            return data.clip;
        }
    }
}