using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class EnemyParameterEntity : BaseParameterEntity
    {
        public void Init(ParameterVO parameter, LevelVO level) => Init(new ParameterVO(parameter, level));

        public EnemyParameterVO ToVO() => new(_parameter, currentHp, additionalHp);
    }
}