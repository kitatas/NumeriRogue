namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class PlayerParameterEntity : ParameterEntity
    {
        public PlayerParameterVO ToVO() => new(_parameter, currentHp);
    }
}