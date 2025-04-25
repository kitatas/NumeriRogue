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

    public sealed class CardVO
    {
        public readonly Suit suit;
        public readonly int rank;

        public CardVO(int suit, int rank)
        {
            this.suit = suit.ToSuit();
            this.rank = rank;
        }

        public string imgPath => ZString.Format("Assets/Externals/Sprites/Cards/cards.png[card_{0}s_{1}]",
            suit.FastToString().ToLower(), rank);
    }

    public sealed class DeckVO
    {
        public readonly List<CardVO> cards;

        public DeckVO(List<CardVO> cards)
        {
            this.cards = cards;
        }
    }

    public sealed class CharacterVO : ICommand
    {
        public readonly CharacterType type;
        public readonly string name;
        public readonly ParameterVO parameter;

        public CharacterVO(int type, int hp, int atk, int def)
        {
            this.type = type.ToCharacterType();
            this.name = this.type.FastToString();
            this.parameter = new ParameterVO(type, hp, atk, def);
        }

        public string objPath =>
            ZString.Format("Assets/PrimeMillionaire/Prefabs/Characters/Character - {0}.prefab", name);

        public string imgPath =>
            ZString.Format("Assets/Externals/Sprites/Characters/boss_{0}.png[boss_{0}_101]", name.ToLower());
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

        public ParameterVO(int type, int hp, int atk, int def, int currentHp)
        {
            this.type = type.ToCharacterType();
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.currentHp = currentHp;
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

    public sealed class SkillBaseVO
    {
        public readonly SkillType type;
        public readonly SkillTarget target;
        public readonly int priceRate;
        public readonly string description;

        public SkillBaseVO(int type, int target, int priceRate, string description)
        {
            this.type = type.ToSkillType();
            this.target = target.ToSkillTarget();
            this.priceRate = priceRate;
            this.description = description;
        }

        public string iconPath => ZString.Format("Assets/Externals/Sprites/Skills/{0}.png[{0}]", target.FastToString());
    }

    public sealed class SkillVO
    {
        public readonly SkillBaseVO skillBase;
        public readonly SkillEffectVO skillEffect;
        public bool isHold;

        public SkillVO(SkillBaseVO skillBase, SkillEffectVO skillEffect)
        {
            this.skillBase = skillBase;
            this.skillEffect = skillEffect;
        }

        public int price => skillEffect.value * skillBase.priceRate;
        public string description => ZString.Format(skillBase.description, skillEffect.value);
    }

    public sealed class SkillEffectVO
    {
        public readonly SkillType type;
        public readonly int value;

        public SkillEffectVO(int type, int value)
        {
            this.type = type.ToSkillType();
            this.value = value;
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

        public string bgPath => ZString.Format("Assets/Externals/Sprites/Stages/{0}/background@2x.jpg[background@2x_0]",
            lowerName);

        public string mgPath =>
            ZString.Format("Assets/Externals/Sprites/Stages/{0}/midground@2x.png[midground@2x_0]", lowerName);

        public string glowPath =>
            ZString.Format("Assets/Externals/Sprites/Stages/{0}/midground_glow@2x.png[midground_glow@2x_0]", lowerName);
    }

    public sealed class DropRateVO
    {
        public readonly int turn;
        public readonly float rate;

        public DropRateVO(int turn, int rate)
        {
            this.turn = turn;
            this.rate = rate / 100.0f;
        }
    }

    public sealed class LevelVO
    {
        public readonly int level;
        public readonly float rate;

        public LevelVO(int level, int rate)
        {
            this.level = level;
            this.rate = rate / 100.0f;
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

    public sealed class InterruptVO
    {
        public readonly ParameterVO player;
        public readonly ParameterVO enemy;
        public readonly DeckVO deck;
        public readonly HoldSkillVO holdSkill;
        public readonly int dollar;
        public readonly int level;
        public readonly int turn;
        public readonly int communityBattlePt;

        public InterruptVO(ParameterVO player, ParameterVO enemy, DeckVO deck, HoldSkillVO holdSkill, int dollar,
            int level, int turn, int communityBattlePt)
        {
            this.player = player;
            this.enemy = enemy;
            this.deck = deck;
            this.holdSkill = holdSkill;
            this.dollar = dollar;
            this.level = level;
            this.turn = turn;
            this.communityBattlePt = communityBattlePt;
        }
    }

    public sealed class ProgressVO
    {
        public readonly List<CharacterProgressVO> characterProgress;

        public ProgressVO(List<CharacterProgressVO> characterProgress)
        {
            this.characterProgress = characterProgress;
        }

        public CharacterProgressVO Find(CharacterType type)
        {
            return characterProgress.Find(x => x.type == type);
        }
    }

    public sealed class CharacterProgressVO
    {
        public readonly CharacterType type;
        public readonly ProgressStatus status;

        public CharacterProgressVO(CharacterType type, ProgressStatus status)
        {
            this.type = type;
            this.status = status;
        }

        public bool isNew => status == ProgressStatus.New;
        public bool isClear => status == ProgressStatus.Clear;
    }

    public sealed class AudioVO
    {
        public readonly AudioClip clip;
        public readonly float duration;

        public AudioVO(AudioClip clip, float duration)
        {
            if (duration < 0.0f) throw new QuitExceptionVO(ExceptionConfig.INVALID_SOUND_DURATION);
            this.clip = clip;
            this.duration = duration;
        }
    }

    public sealed class SoundVO
    {
        public readonly VolumeVO master;
        public readonly VolumeVO bgm;
        public readonly VolumeVO se;

        public SoundVO(VolumeVO master, VolumeVO bgm, VolumeVO se)
        {
            this.master = master;
            this.bgm = bgm;
            this.se = se;
        }
    }

    public sealed class VolumeVO
    {
        public readonly float volume;
        public readonly bool isMute;

        public VolumeVO(float volume, bool isMute)
        {
            this.volume = volume;
            this.isMute = isMute;
        }

        public VolumeVO Multiply(VolumeVO other)
        {
            return new VolumeVO(volume * other.volume, isMute || other.isMute);
        }
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