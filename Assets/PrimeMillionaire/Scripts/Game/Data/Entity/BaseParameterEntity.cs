using PrimeMillionaire.Common;
using UnityEngine;

namespace PrimeMillionaire.Game.Data.Entity
{
    public abstract class BaseParameterEntity
    {
        protected ParameterVO _parameter;
        private int _hp;

        public virtual int maxHp => _parameter.hp;
        public virtual int atk => _parameter.atk;
        public virtual int def => _parameter.def;
        public virtual int currentHp => _hp;

        public void Init(ParameterVO parameter)
        {
            SetParameter(parameter);
            SetHp(parameter.hp);
        }

        public void InitForInterrupt(ParameterVO parameter)
        {
            SetParameter(parameter);
            SetHp(parameter.currentHp);
        }

        public void SetParameter(ParameterVO parameter) => _parameter = parameter;
        public void SetHp(int hp) => _hp = hp;

        public void Heal(int value) => _hp = Mathf.Min(maxHp, currentHp + value);
        public void Damage(int value) => _hp = Mathf.Max(0, currentHp - Mathf.Max(0, value));
    }
}