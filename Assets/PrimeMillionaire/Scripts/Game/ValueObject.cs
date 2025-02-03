using System.Collections.Generic;
using PrimeMillionaire.Common;
using VitalRouter;

namespace PrimeMillionaire.Game
{
    public sealed class PlayerParameterVO : ParameterVO, ICommand
    {
        public PlayerParameterVO(int type, int hp, int atk, int def) : base(type, hp, atk, def)
        {
        }

        public PlayerParameterVO(ParameterVO parameter, int currentHp, int additionalHp) : base(parameter, currentHp,
            additionalHp)
        {
        }
    }

    public sealed class EnemyParameterVO : ParameterVO, ICommand
    {
        public EnemyParameterVO(int type, int hp, int atk, int def) : base(type, hp, atk, def)
        {
        }

        public EnemyParameterVO(ParameterVO parameter, int currentHp, int additionalHp) : base(parameter, currentHp,
            additionalHp)
        {
        }
    }

    public sealed class HandVO
    {
        public readonly int index;
        public readonly CardVO card;

        public HandVO(int index, CardVO card)
        {
            this.index = index;
            this.card = card;
        }
    }

    public sealed class OrderVO
    {
        public readonly CardVO card;

        public OrderVO()
        {
            card = null;
        }

        public OrderVO(CardVO card)
        {
            this.card = card;
        }
    }

    public sealed class OrderValueVO : ICommand
    {
        public readonly int value;
        public readonly BonusVO bonus;

        public OrderValueVO(int value, BonusVO bonus)
        {
            this.value = value;
            this.bonus = bonus;
        }
    }

    public sealed class OrderCardsFadeVO : ICommand
    {
        public readonly Fade fade;
        public readonly float duration;

        public OrderCardsFadeVO(Fade fade, float duration)
        {
            this.fade = fade;
            this.duration = duration;
        }
    }

    public sealed class BonusVO
    {
        public readonly List<BonusType> types;

        public BonusVO(List<BonusType> types)
        {
            this.types = types;
        }
    }

    public sealed class BattlePtVO : ICommand
    {
        public readonly Side side;
        public readonly int value;

        public BattlePtVO(Side side, int value)
        {
            this.side = side;
            this.value = value;
        }
    }

    public sealed class PickSkillVO : ICommand
    {
        public readonly int index;
        public readonly SkillVO skill;

        public PickSkillVO(int index, SkillVO skill)
        {
            this.index = index;
            this.skill = skill;
        }
    }

    public sealed class HoldSkillVO : ICommand
    {
        public readonly List<SkillVO> skills;

        public HoldSkillVO(List<SkillVO> skills)
        {
            this.skills = skills;
            foreach (var skill in this.skills)
            {
                skill.isHold = true;
            }
        }
    }

    public sealed class ModalVO : ICommand
    {
        public readonly ModalType type;
        public readonly bool isActivate;

        public ModalVO(ModalType type, bool isActivate)
        {
            this.type = type;
            this.isActivate = isActivate;
        }
    }
}