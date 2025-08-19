using System.Collections.Generic;
using PrimeMillionaire.Common;
using UnityEngine;
using VitalRouter;

namespace PrimeMillionaire.Game
{
    public sealed class PlayerParameterVO : ParameterVO, ICommand
    {
        public PlayerParameterVO(int type, int hp, int atk, int def) : base(type, hp, atk, def)
        {
        }

        public PlayerParameterVO(ParameterVO parameter, int currentHp) : base(parameter, currentHp)
        {
        }
    }

    public sealed class EnemyParameterVO : ParameterVO, ICommand
    {
        public EnemyParameterVO(int type, int hp, int atk, int def) : base(type, hp, atk, def)
        {
        }

        public EnemyParameterVO(ParameterVO parameter, int currentHp) : base(parameter, currentHp)
        {
        }
    }

    public sealed class HandVO
    {
        public readonly int deckIndex;
        public readonly int handIndex;
        public readonly CardVO card;

        public HandVO(int deckIndex, int handIndex, CardVO card)
        {
            this.deckIndex = deckIndex;
            this.handIndex = handIndex;
            this.card = card;
        }
    }

    public sealed class SortHandVO : ICommand
    {
        public readonly Side side;
        public readonly List<HandVO> hands;

        public SortHandVO(Side side, List<HandVO> hands)
        {
            this.side = side;
            this.hands = hands;
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
        public readonly List<BonusVO> bonus;

        public OrderValueVO(int value, List<BonusVO> bonus)
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
            if (fade == Fade.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_FADE);
            this.fade = fade;
            this.duration = duration;
        }
    }

    public sealed class BuffVO : ICommand
    {
        public readonly Side side;
        public readonly GameObject fxObject;

        public BuffVO(Side side, GameObject fxObject)
        {
            this.side = side;
            this.fxObject = fxObject;
        }
    }

    public sealed class BattlePtVO : ICommand
    {
        public readonly Side side;
        public readonly int value;

        public BattlePtVO(Side side, int value)
        {
            if (side == Side.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_FADE);
            this.side = side;
            this.value = value;
        }
    }

    public sealed class BattleAnimationVO : ICommand
    {
        public readonly Side side;
        public readonly BattleAnim battleAnim;

        public BattleAnimationVO(Side side, BattleAnim battleAnim)
        {
            if (side == Side.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_FADE);
            if (battleAnim == BattleAnim.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_BATTLE_ANIMATION);
            this.side = side;
            this.battleAnim = battleAnim;
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

    public sealed class ModalVO : BaseModalVO<ModalType>
    {
        public ModalVO(ModalType type, bool isActivate) : base(type, isActivate)
        {
        }
    }
}