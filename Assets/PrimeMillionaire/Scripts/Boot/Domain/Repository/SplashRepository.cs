using PrimeMillionaire.Boot.Data.DataStore;
using PrimeMillionaire.Common;
using UnityEngine;

namespace PrimeMillionaire.Boot.Domain.Repository
{
    public sealed class SplashRepository
    {
        private readonly SplashTable _splashTable;

        public SplashRepository(SplashTable splashTable)
        {
            _splashTable = splashTable;
        }

        public Sprite Find(SplashType type)
        {
            var data = _splashTable.all.Find(x => x.type == type);
            if (data == null)
            {
                throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SPLASH);
            }

            return data.sprite;
        }
    }
}