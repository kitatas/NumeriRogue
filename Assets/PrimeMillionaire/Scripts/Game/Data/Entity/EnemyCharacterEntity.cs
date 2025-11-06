using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class EnemyCharacterEntity
    {
        private int _id;

        public int id => _id;

        public void SetType(int value)
        {
            if (value == 0) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_CHARACTER);
            _id = value;
        }
    }
}