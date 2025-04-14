using System;
using System.Collections.Generic;
using Cysharp.Text;
using FastEnumUtility;
using PrimeMillionaire.Common.Utility;
using UnityEngine;
using VitalRouter;

namespace PrimeMillionaire.Common
{
    public sealed class LoadVO
    {
        public readonly SceneName sceneName;
        public readonly LoadType loadType;

        public LoadVO(SceneName sceneName, LoadType loadType)
        {
            if (sceneName == SceneName.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SCENE);
            if (loadType == LoadType.None) throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_LOAD);
            this.sceneName = sceneName;
            this.loadType = loadType;
        }

        public string name => sceneName.FastToString();
        public bool isFade => loadType == LoadType.Fade;
    }

    [Serializable]
    public sealed class CardVO
    {
        public Suit suit;
        public int rank;

        public CardVO(int suit, int rank)
        {
            this.suit = suit.ToSuit();
            this.rank = rank;
        }

        public string imgPath => ZString.Format("Assets/Externals/Sprites/Cards/cards.png[card_{0}s_{1}]", suit.FastToString().ToLower(), rank);
    }

    [Serializable]
    public sealed class DeckVO
    {
        public List<CardVO> cards;

        public DeckVO(List<CardVO> cards)
        {
            this.cards = cards;
        }
    }

    public sealed class CharacterVO : ICommand
    {
        public readonly CharacterType type;
        public readonly string name;
        public readonly StageVO stage;
        public readonly ParameterVO parameter;

        public CharacterVO(int type, int stage, int hp, int atk, int def)
        {
            this.type = type.ToCharacterType();
            this.name = this.type.FastToString();
            this.stage = new StageVO(stage);
            this.parameter = new ParameterVO(type, hp, atk, def);
        }

        public string objPath => ZString.Format("Assets/PrimeMillionaire/Prefabs/Characters/Character - {0}.prefab", name);
        public string imgPath => ZString.Format("Assets/Externals/Sprites/Characters/boss_{0}.png[boss_{0}_101]", name.ToLower());
    }

    [Serializable]
    public class ParameterVO
    {
        public CharacterType type;
        public int hp;
        public int atk;
        public int def;
        public int currentHp;

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

        public ParameterVO(ParameterVO parameter, LevelVO level)
        {
            this.type = parameter.type;
            this.hp = Mathf.CeilToInt(parameter.hp * level.rate);
            this.atk = Mathf.CeilToInt(parameter.atk * level.rate);
            this.def = Mathf.CeilToInt(parameter.def * level.rate);
            this.currentHp = Mathf.CeilToInt(parameter.currentHp * level.rate);
        }

        public string name => type.FastToString();
        public float hpRate => (float)currentHp / hp;
    }

    [Serializable]
    public sealed class SkillVO
    {
        public SkillType type;
        public string icon;
        public int price;
        public string description;
        public SkillEffectVO effect;
        public bool isHold;

        public SkillVO(int type, string icon, int priceRate, string description, SkillEffectVO effect)
        {
            this.type = type.ToSkillType();
            this.icon = icon;
            this.price = effect.value * priceRate;
            this.description = ZString.Format(description, effect.value);
            this.effect = effect;
        }

        public string iconPath => ZString.Format("Assets/Externals/Sprites/Skills/{0}.png[{0}]", icon);
    }

    [Serializable]
    public sealed class SkillEffectVO
    {
        public SkillType type;
        public int value;

        public SkillEffectVO(int type, int value)
        {
            this.type = type.ToSkillType();
            this.value = value;
        }
    }

    [Serializable]
    public sealed class HoldSkillVO : ICommand
    {
        public List<SkillVO> skills;

        public HoldSkillVO(List<SkillVO> skills)
        {
            this.skills = skills;
            foreach (var skill in this.skills)
            {
                skill.isHold = true;
            }
        }
    }

    public sealed class StageVO
    {
        public readonly StageType type;
        public readonly string name;
        public readonly string lowerName;

        public StageVO(int type)
        {
            this.type = type.ToStageType();
            this.name = this.type.FastToString();
            this.lowerName = this.name.ToLower();
        }

        public bool hasGlow => type is StageType.Abyssian or StageType.Redrock;

        public string bgPath => ZString.Format("Assets/Externals/Sprites/Stages/{0}/background@2x.jpg[background@2x_0]", lowerName);
        public string mgPath => ZString.Format("Assets/Externals/Sprites/Stages/{0}/midground@2x.png[midground@2x_0]", lowerName);
        public string glowPath => ZString.Format("Assets/Externals/Sprites/Stages/{0}/midground_glow@2x.png[midground_glow@2x_0]", lowerName);
    }

    public sealed class DropRateVO
    {
        public readonly int turn;
        public readonly float dropRate;

        public DropRateVO(int turn, float dropRate)
        {
            this.turn = turn;
            this.dropRate = dropRate;
        }
    }

    public sealed class LevelVO
    {
        public readonly int level;
        public readonly float rate;

        public LevelVO(int level, float rate)
        {
            this.level = level;
            this.rate = rate;
        }
    }

    public sealed class BonusVO
    {
        public readonly BonusType type;
        public readonly float value;

        public BonusVO(int type, int rate)
        {
            this.type = type.ToBonusType();
            this.value = rate / 100.0f;
        }
    }

    [Serializable]
    public sealed class InterruptVO
    {
        public CharacterType playerCharacter;
        public ParameterVO playerParameter;
        public CharacterType enemyCharacter;
        public ParameterVO enemyParameter;
        public int enemyCount;
        public int turn;
        public DeckVO deck;
        public int communityBattlePt;
        public int dollar;
        public HoldSkillVO holdSkill;

        public InterruptVO(CharacterType playerCharacter, ParameterVO playerParameter, CharacterType enemyCharacter,
            ParameterVO enemyParameter, int enemyCount, int turn, DeckVO deck, int communityBattlePt, int dollar,
            HoldSkillVO holdSkill)
        {
            this.playerCharacter = playerCharacter;
            this.playerParameter = playerParameter;
            this.enemyCharacter = enemyCharacter;
            this.enemyParameter = enemyParameter;
            this.enemyCount = enemyCount;
            this.turn = turn;
            this.deck = deck;
            this.communityBattlePt = communityBattlePt;
            this.dollar = dollar;
            this.holdSkill = holdSkill;
        }
    }

    [Serializable]
    public sealed class ProgressVO
    {
        public List<CharacterProgressVO> characterProgress;

        public ProgressVO(List<CharacterProgressVO> characterProgress)
        {
            this.characterProgress = characterProgress;
        }

        public CharacterProgressVO Find(CharacterType type)
        {
            return characterProgress.Find(x => x.type == type);
        }
    }

    [Serializable]
    public sealed class CharacterProgressVO
    {
        public CharacterType type;
        public ProgressStatus status;

        public CharacterProgressVO(CharacterType type, ProgressStatus status)
        {
            this.type = type;
            this.status = status;
        }

        public bool isNew => status == ProgressStatus.New;
        public bool isClear => status == ProgressStatus.Clear;
    }

    public abstract class BaseModalVO<T> : ICommand where T : Enum
    {
        public readonly T type;
        public readonly bool isActivate;

        public BaseModalVO(T type, bool isActivate)
        {
            this.type = type;
            this.isActivate = isActivate;
        }
    }

    public abstract class ExceptionVO : Exception, ICommand
    {
        public ExceptionVO(string message) : base(message)
        {
        }

        public virtual string exceptionMessage => ExceptionConfig.QUIT_MESSAGE;
        public string message => ZString.Format("{0}\n{1}", base.Message, exceptionMessage);
    }

    public sealed class RebootExceptionVO : ExceptionVO
    {
        public RebootExceptionVO(string message) : base(message)
        {
        }

        public override string exceptionMessage => ExceptionConfig.REBOOT_MESSAGE;
    }

    public sealed class RetryExceptionVO : ExceptionVO
    {
        public RetryExceptionVO(string message) : base(message)
        {
        }

        public override string exceptionMessage => ExceptionConfig.RETRY_MESSAGE;
    }

    public sealed class QuitExceptionVO : ExceptionVO
    {
        public QuitExceptionVO(string message) : base(message)
        {
        }
    }
}