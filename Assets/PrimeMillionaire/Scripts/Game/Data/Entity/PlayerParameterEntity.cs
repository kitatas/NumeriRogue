namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class PlayerParameterEntity : BaseParameterEntity
    {
        public PlayerParameterVO ToVO() => new(_parameter, currentHp, additionalHp);
    }
}