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

        public static Suit ToSuit(this int self)
        {
            return FastEnum.IsDefined<Suit>(self)
                ? (Suit)self
                : throw new Exception();
        }

        public static CharacterType ToCharacterType(this int self)
        {
            return FastEnum.IsDefined<CharacterType>(self)
                ? (CharacterType)self
                : throw new Exception();
        }

        public static float ToBonus(this BonusType type)
        {
            return type switch
            {
                BonusType.PrimeNumber => 2.0f,
                BonusType.Suit => 1.5f,
                BonusType.ValueDown => 0.5f,
                _ => throw new Exception(),
            };
        }
    }
}