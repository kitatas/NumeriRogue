using FastEnumUtility;
using PrimeMillionaire.Game.Utility;
using UnityEngine;
using VitalRouter;

namespace PrimeMillionaire.Game
{
    public sealed class CardVO
    {
        public readonly Suit suit;
        public readonly int rank;
        public readonly string imgPath;
        public readonly Sprite sprite;

        public CardVO(Suit suit, int rank, Sprite sprite)
        {
            this.suit = suit;
            this.rank = rank;
            this.sprite = sprite;
        }

        public CardVO(Suit suit, int rank, string imgPath)
        {
            this.suit = suit;
            this.rank = rank;
            this.imgPath = imgPath;
        }
    }

    public sealed class CharacterVO
    {
        public readonly CharacterType type;
        public readonly string objPath;

        public CharacterVO(int type, string objPath)
        {
            this.type = type.ToCharacterType();
            this.objPath = objPath;
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

        public OrderValueVO(int value)
        {
            this.value = value;
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
}