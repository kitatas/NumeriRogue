using System;
using FastEnumUtility;

namespace PrimeMillionaire.Game.Utility
{
    public static class CustomExtension
    {
        public static int ToSign(this Side side)
        {
            return side switch
            {
                Side.Player => -1,
                Side.Enemy => 1,
                _ => throw new Exception(),
            };
        }

        public static CharacterType ToCharacterType(this int self)
        {
            return FastEnum.IsDefined<CharacterType>(self)
                ? (CharacterType)self
                : throw new Exception();
        }
    }
}