using System;
using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class EnemyCharacterEntity
    {
        private CharacterType _type;

        public CharacterType type => _type;

        public void SetType(CharacterType value)
        {
            if (value == CharacterType.None) throw new Exception();
            _type = value;
        }
    }
}