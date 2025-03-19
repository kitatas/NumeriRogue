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

        public static float ToBonus(this BonusType type)
        {
            return type switch
            {
                BonusType.PrimeNumber => 2.0f,
                BonusType.SuitMatch => 1.5f,
                BonusType.ValueDown => 0.5f,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BONUS),
            };
        }
    }
}