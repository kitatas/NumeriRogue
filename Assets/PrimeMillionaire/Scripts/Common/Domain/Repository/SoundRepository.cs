using PrimeMillionaire.Common.Data.DataStore;
using UnityEngine;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class SoundRepository
    {
        private readonly BgmTable _bgmTable;

        public SoundRepository(BgmTable bgmTable)
        {
            _bgmTable = bgmTable;
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
    }
}