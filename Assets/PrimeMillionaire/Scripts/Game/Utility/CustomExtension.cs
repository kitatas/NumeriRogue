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

        public static BonusType ToBonusType(this PokerHands self)
        {
            return self switch
            {
                PokerHands.HighCard => BonusType.HighCard,
                PokerHands.OnePair => BonusType.OnePair,
                PokerHands.Flush => BonusType.Flush,
                PokerHands.Straight => BonusType.Straight,
                PokerHands.ThreeOfAKind => BonusType.ThreeOfAKind,
                PokerHands.StraightFlush => BonusType.StraightFlush,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_POKER_HANDS)
            };
        }
    }
}