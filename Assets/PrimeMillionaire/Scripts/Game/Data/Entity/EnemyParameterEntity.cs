namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class EnemyParameterEntity : BaseParameterEntity
    {
        public EnemyParameterVO ToVO() => new(_parameter, currentHp);
    }
}