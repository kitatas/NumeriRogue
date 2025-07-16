using System;
using PrimeMillionaire.Common;

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
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }

        public static SkillType[] ToSkillTypes(this BonusType self)
        {
            return self switch
            {
                BonusType.ValueUp => new[] { SkillType.ValueUpAtk, SkillType.ValueUpDef },
                BonusType.ValueDown => new[] { SkillType.ValueDownDollar, SkillType.ValueDownHeal },
                BonusType.HighCard => new[] { SkillType.HighCard, SkillType.HighCardAtk, SkillType.HighCardDef },
                BonusType.OnePair => new[] { SkillType.PokerHands, SkillType.PokerHandsAtk, SkillType.PokerHandsDef, SkillType.OnePair },
                BonusType.Flush => new[] { SkillType.PokerHands, SkillType.PokerHandsAtk, SkillType.PokerHandsDef, SkillType.Flush },
                BonusType.Straight => new[] { SkillType.PokerHands, SkillType.PokerHandsAtk, SkillType.PokerHandsDef, SkillType.Straight },
                BonusType.ThreeOfAKind => new[] { SkillType.PokerHands, SkillType.PokerHandsAtk, SkillType.PokerHandsDef, SkillType.ThreeOfAKind },
                BonusType.StraightFlush => new[] { SkillType.PokerHands, SkillType.PokerHandsAtk, SkillType.PokerHandsDef, SkillType.StraightFlush },
                BonusType.Odd => new[] { SkillType.Odd, SkillType.OddAtk, SkillType.OddDef },
                BonusType.Even => new[] { SkillType.Even, SkillType.EvenAtk, SkillType.EvenDef },
                BonusType.NotSpecialNumbers => new[] { SkillType.NotSpecialNumbers, SkillType.NotSpecialNumbersAtk, SkillType.NotSpecialNumbersDef },
                BonusType.SpecialNumbers => new[] { SkillType.SpecialNumbers, SkillType.SpecialNumbersAtk, SkillType.SpecialNumbersDef },
                BonusType.PrimeNumber => new[] { SkillType.PrimeNumber },
                BonusType.PowerOfTwo => new[] { SkillType.PowerOfTwo },
                BonusType.PerfectNumber => new[] { SkillType.PerfectNumber },
                BonusType.PalindromicNumber => new[] { SkillType.PalindromicNumber },
                BonusType.SquareNumber => new[] { SkillType.SquareNumber },
                BonusType.TriangularNumber => new[] { SkillType.TriangularNumber },
                BonusType.FibonacciNumber => new[] { SkillType.FibonacciNumber },
                BonusType.HarshadNumber => new[] { SkillType.HarshadNumber },
                BonusType.MersenneNumber => new[] { SkillType.MersenneNumber },
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BONUS),
            };
        }
    }
}