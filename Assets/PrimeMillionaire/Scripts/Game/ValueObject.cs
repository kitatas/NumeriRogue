using System.Collections.Generic;
using FastEnumUtility;
using PrimeMillionaire.Game.Utility;
using VitalRouter;

namespace PrimeMillionaire.Game
{
    public sealed class CardVO
    {
        public readonly Suit suit;
        public readonly int rank;
        public readonly string imgPath;

        public CardVO(int suit, int rank)
        {
            var s = suit.ToSuit();
            this.suit = s;
            this.rank = rank;
            this.imgPath = $"Assets/Externals/Sprites/Cards/cards.png[card_{s.FastToString().ToLower()}s_{rank}]";
        }
    }

    public sealed class CharacterVO
    {
        public readonly CharacterType type;
        public readonly string objPath;

        public CharacterVO(int type)
        {
            var t = type.ToCharacterType();
            this.type = t;
            this.objPath = $"Assets/PrimeMillionaire/Prefabs/Characters/{t.FastToString()}.prefab";
        }
    }

    public class ParameterVO
    {
        public readonly CharacterType type;
        public readonly int hp;
        public readonly int atk;
        public readonly int def;
        public readonly int currentHp;

        public ParameterVO(int type, int hp, int atk, int def)
        {
            this.type = type.ToCharacterType();
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.currentHp = hp;
        }

        public ParameterVO(ParameterVO parameter, int currentHp)
        {
            this.type = parameter.type;
            this.hp = parameter.hp;
            this.atk = parameter.atk;
            this.def = parameter.def;
            this.currentHp = currentHp;
        }

        public string name => type.FastToString();
        public float hpRate => (float)currentHp / hp;
    }

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

    public sealed class SkillVO
    {
        public readonly SkillType type;
        public readonly int value;
        public readonly string description;

        public SkillVO(int type, int value)
        {
            this.type = type.ToSkillType();
            this.value = value;
            this.description = this.type.ToDescription(value);
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
}