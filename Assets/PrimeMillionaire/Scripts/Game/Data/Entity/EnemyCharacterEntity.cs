using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class EnemyCharacterEntity
    {
        private CharacterType _type;

        public CharacterType type => _type;

        public void SetType(CharacterType value)
        {
            if (value == CharacterType.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_CHARACTER);
            _type = value;
        }
    }
}