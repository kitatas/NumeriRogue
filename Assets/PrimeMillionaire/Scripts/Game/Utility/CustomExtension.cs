using System;

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
    }
}