using FastEnumUtility;
using PrimeMillionaire.Common.Utility;

namespace PrimeMillionaire.Common
{
    public sealed class LoadVO
    {
        public readonly SceneName sceneName;
        public readonly LoadType loadType;

        public LoadVO(SceneName sceneName, LoadType loadType)
        {
            this.sceneName = sceneName;
            this.loadType = loadType;
        }

        public string name => sceneName.FastToString();
        public bool isFade => loadType == LoadType.Fade;
    }

    public sealed class CardVO
    {
        public readonly Suit suit;
        public readonly int rank;
        public readonly string imgPath;

        public CardVO(int suit, int rank)
        {
            this.suit = suit.ToSuit();
            this.rank = rank;
            this.imgPath = $"Assets/Externals/Sprites/Cards/cards.png[card_{this.suit.FastToString().ToLower()}s_{rank}]";
        }
    }

    public sealed class CharacterVO
    {
        public readonly CharacterType type;
        public readonly string objPath;

        public CharacterVO(int type)
        {
            this.type = type.ToCharacterType();
            this.objPath = $"Assets/PrimeMillionaire/Prefabs/Characters/Character - {this.type.FastToString()}.prefab";
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

        public ParameterVO(ParameterVO parameter, int currentHp, int additionalHp)
        {
            this.type = parameter.type;
            this.hp = parameter.hp + additionalHp;
            this.atk = parameter.atk;
            this.def = parameter.def;
            this.currentHp = currentHp + additionalHp;
        }

        public string name => type.FastToString();
        public float hpRate => (float)currentHp / hp;
    }

    public sealed class SkillVO
    {
        public readonly SkillType type;
        public readonly int value;
        public readonly string description;
        public readonly int price;
        public bool isHold;

        public SkillVO(int type, int value)
        {
            this.type = type.ToSkillType();
            this.value = value;
            this.description = this.type.ToDescription(value);
            this.price = this.type.ToPrice(value);
        }
    }
}