namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class EnemyParameterEntity : ParameterEntity
    {
        public EnemyParameterVO ToVO() => new(_parameter, currentHp);
    }
}