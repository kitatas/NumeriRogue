using System;
using System.Collections.Generic;
using System.Linq;
using FastEnumUtility;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [Serializable]
    public sealed class InterruptDTO
    {
        public ParameterDTO player;
        public ParameterDTO enemy;
        public DeckDTO deck;
        public HoldSkillDTO holdSkill;
        public LotSkillDTO lotSkill;
        public int dollar;
        public int level;
        public int turn;
        public int enemyCount;
        public int communityBattlePt;

        public InterruptDTO(InterruptVO interrupt)
        {
            player = new ParameterDTO(interrupt.player);
            enemy = new ParameterDTO(interrupt.enemy);
            deck = new DeckDTO(interrupt.deck);
            holdSkill = new HoldSkillDTO(interrupt.holdSkill);
            lotSkill = new LotSkillDTO(interrupt.lotSkill);
            dollar = interrupt.dollar;
            level = interrupt.level;
            turn = interrupt.turn;
            enemyCount = interrupt.enemyCount;
            communityBattlePt = interrupt.communityBattlePt;
        }

        public InterruptVO ToVO() => new(
            player.ToVO(),
            enemy.ToVO(),
            deck.ToVO(),
            holdSkill.ToVO(),
            lotSkill.ToVO(),
            dollar,
            level,
            turn,
            enemyCount,
            communityBattlePt
        );
    }

    [Serializable]
    public sealed class ParameterDTO
    {
        public CharacterType type;
        public int hp;
        public int atk;
        public int def;
        public int currentHp;

        public ParameterDTO(ParameterVO parameter)
        {
            type = parameter.type;
            hp = parameter.hp;
            atk = parameter.atk;
            def = parameter.def;
            currentHp = parameter.currentHp;
        }

        public ParameterVO ToVO() => new(type.ToInt32(), hp, atk, def, currentHp);
    }

    [Serializable]
    public sealed class DeckDTO
    {
        public List<CardDTO> cards;

        public DeckDTO(DeckVO deck)
        {
            cards = deck.cards.Select(x => new CardDTO(x)).ToList();
        }

        public DeckVO ToVO() => new(cards.Select(x => x.ToVO()).ToList());
    }

    [Serializable]
    public sealed class CardDTO
    {
        public Suit suit;
        public int rank;

        public CardDTO(CardVO card)
        {
            suit = card.suit;
            rank = card.rank;
        }

        public CardVO ToVO() => new(suit.ToInt32(), rank);
    }

    [Serializable]
    public sealed class HoldSkillDTO
    {
        public List<SkillDTO> skills;

        public HoldSkillDTO(HoldSkillVO holdSkill)
        {
            skills = holdSkill.skills.Select(x => new SkillDTO(x)).ToList();
        }

        public HoldSkillVO ToVO() => new(skills.Select(x => x.ToVO()).ToList());
    }

    [Serializable]
    public sealed class LotSkillDTO
    {
        public List<SkillDTO> skills;

        public LotSkillDTO(LotSkillVO lotSkill)
        {
            skills = lotSkill.skills.Select(x => new SkillDTO(x)).ToList();
        }

        public LotSkillVO ToVO() => new(skills.Select(x => x.ToVO()).ToList());
    }

    [Serializable]
    public sealed class SkillDTO
    {
        public SkillBaseDTO skillBase;
        public SkillEffectDTO skillEffect;
        public bool isHold;

        public SkillDTO(SkillVO skill)
        {
            skillBase = new SkillBaseDTO(skill.skillBase);
            skillEffect = new SkillEffectDTO(skill.skillEffect);
            isHold = skill.isHold;
        }

        public SkillVO ToVO() => new(skillBase.ToVO(), skillEffect.ToVO()) { isHold = isHold };
    }

    [Serializable]
    public sealed class SkillBaseDTO
    {
        public SkillType type;
        public SkillTarget target;
        public int priceRate;
        public string description;

        public SkillBaseDTO(SkillBaseVO skillBase)
        {
            type = skillBase.type;
            target = skillBase.target;
            priceRate = skillBase.priceRate;
            description = skillBase.description;
        }

        public SkillBaseVO ToVO() => new(type.ToInt32(), target.ToInt32(), priceRate, description);
    }

    [Serializable]
    public sealed class SkillEffectDTO
    {
        public SkillType type;
        public int value;

        public SkillEffectDTO(SkillEffectVO skillEffect)
        {
            type = skillEffect.type;
            value = skillEffect.value;
        }

        public SkillEffectVO ToVO() => new(type.ToInt32(), value);
    }
}