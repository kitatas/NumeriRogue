namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class HandEntity
    {
        public readonly int[] hands;

        public HandEntity()
        {
            hands = new int[HandConfig.MAX_NUM];
        }

        public void Set(int index, int value)
        {
            hands[index] = value;
        }
    }
}