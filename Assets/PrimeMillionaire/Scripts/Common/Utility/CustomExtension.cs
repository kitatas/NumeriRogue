using System;
using FastEnumUtility;

namespace PrimeMillionaire.Common.Utility
{
    public static class CustomExtension
    {
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

        public static SkillType ToSkillType(this int self)
        {
            return FastEnum.IsDefined<SkillType>(self)
                ? (SkillType)self
                : throw new Exception();
        }

        public static string ToDescription(this SkillType self, int value)
        {
            return self switch
            {
                SkillType.HpUp => $"HP {value}% UP",
                SkillType.AtkUp => $"ATK {value}% UP",
                SkillType.DefUp => $"DEF {value}% UP",
                _ => throw new Exception(),
            };
        }

        public static int ToPrice(this SkillType self, int value)
        {
            return self switch
            {
                SkillType.HpUp => value * 8,
                SkillType.AtkUp => value * 7,
                SkillType.DefUp => value * 5,
                _ => throw new Exception(),
            };
        }

        public static StageType ToStageType(this int self)
        {
            return FastEnum.IsDefined<StageType>(self)
                ? (StageType)self
                : throw new Exception();
        }
    }
}