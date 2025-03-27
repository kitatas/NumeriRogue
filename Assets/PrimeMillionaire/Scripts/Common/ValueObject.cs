using System;
using System.Collections.Generic;
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
        public string imgPath;

        public CardVO(int suit, int rank)
        {
            this.suit = suit.ToSuit();
            this.rank = rank;
            this.imgPath = $"Assets/Externals/Sprites/Cards/cards.png[card_{this.suit.FastToString().ToLower()}s_{rank}]";
        }
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

        public string objPath => $"Assets/PrimeMillionaire/Prefabs/Characters/Character - {name}.prefab";
        public string imgPath => $"Assets/Externals/Sprites/Characters/boss_{name.ToLower()}.png[boss_{name.ToLower()}_101]";
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

        public ParameterVO(ParameterVO parameter, int currentHp, int additionalHp)
        {
            this.type = parameter.type;
            this.hp = parameter.hp + additionalHp;
            this.atk = parameter.atk;
            this.def = parameter.def;
            this.currentHp = currentHp + additionalHp;
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
        public int value;
        public string description;
        public int price;
        public bool isHold;

        public SkillVO(int type, int value)
        {
            this.type = type.ToSkillType();
            this.value = value;
            this.description = this.type.ToDescription(value);
            this.price = this.type.ToPrice(value);
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

        public string bgPath => $"Assets/Externals/Sprites/Stages/{lowerName}/background@2x.jpg[background@2x_0]";
        public string mgPath => $"Assets/Externals/Sprites/Stages/{lowerName}/midground@2x.png[midground@2x_0]";
        public string glowPath => $"Assets/Externals/Sprites/Stages/{lowerName}/midground_glow@2x.png[midground_glow@2x_0]";
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

    public sealed class LicenseVO
    {
        public readonly string title;
        public readonly string content;

        public LicenseVO(string title, string content)
        {
            this.title = title;
            this.content = content;
        }
    }

    public abstract class ExceptionVO : Exception, ICommand
    {
        public ExceptionVO(string message) : base(message)
        {
        }
        
        public virtual string message => $"{base.Message}\n{ExceptionConfig.QUIT_MESSAGE}";
    }

    public sealed class RebootExceptionVO : ExceptionVO
    {
        public RebootExceptionVO(string message) : base(message)
        {
        }

        public override string message => $"{base.Message}\n{ExceptionConfig.REBOOT_MESSAGE}";
    }

    public sealed class RetryExceptionVO : ExceptionVO
    {
        public RetryExceptionVO(string message) : base(message)
        {
        }

        public override string message => $"{base.Message}\n{ExceptionConfig.RETRY_MESSAGE}";
    }

    public sealed class QuitExceptionVO : ExceptionVO
    {
        public QuitExceptionVO(string message) : base(message)
        {
        }

        public override string message => $"{base.Message}\n{ExceptionConfig.QUIT_MESSAGE}";
    }
}