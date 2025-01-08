using UnityEngine;

namespace PrimeMillionaire.Game.Data.Entity
{
    public abstract class ParameterEntity
    {
        private ParameterVO _parameter;
        private int _hp;

        public virtual int maxHp => _parameter.hp;
        public virtual int atk => _parameter.atk;
        public virtual int def => _parameter.def;
        public virtual int currentHp => _hp;

        public void SetParameter(ParameterVO parameter) => _parameter = parameter;

        public void Heal(int value) => _hp = Mathf.Min(maxHp, currentHp + value);
        public void Damage(int value) => _hp = Mathf.Max(0, currentHp - value);
    }
}