using System.Linq;
using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using ObservableCollections;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using PrimeMillionaire.Game.Data.Entity;
using PrimeMillionaire.Game.Domain.Repository;
using UniEx;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class OrderUseCase
    {
        private readonly BonusEntity _bonusEntity;
        private readonly BuffEntity _buffEntity;
        private readonly CommunityBattlePtEntity _communityBattlePtEntity;
        private readonly NumericRepository _numericRepository;
        private readonly ObservableList<OrderVO> _orders;

        public OrderUseCase(BonusEntity bonusEntity, BuffEntity buffEntity,
            CommunityBattlePtEntity communityBattlePtEntity, NumericRepository numericRepository)
        {
            _bonusEntity = bonusEntity;
            _buffEntity = buffEntity;
            _communityBattlePtEntity = communityBattlePtEntity;
            _numericRepository = numericRepository;
            _orders = new ObservableList<OrderVO>(HandConfig.ORDER_NUM)
            {
                new(),
                new(),
                new(),
            };
        }

        public ObservableList<OrderVO> orders => _orders;

        public int Set(CardVO card)
        {
            var orderList = _orders.ToList();
            var o = orderList.Find(x => x.card == card);
            if (o == null)
            {
                // 選択
                var i = orderList.IndexOf(orderList.Find(x => x.card == null));
                _orders[i] = new OrderVO(card);
                return i + 1;
            }
            else
            {
                // 選択解除
                var i = orderList.IndexOf(o);
                _orders[i] = new OrderVO();
                return i + 1;
            }
        }

        public async UniTask RefreshAsync(CancellationToken token)
        {
            for (int i = 0; i < _orders.Count; i++)
            {
                _orders[i] = new OrderVO();
            }

            await UniTaskHelper.DelayAsync(1.0f, token);
        }

        public int currentValue => _orders.Any(x => x.card == null)
            ? 0
            : int.Parse(ZString.Concat(_orders.Select(x => x.card.rank)));

        public int currentValueWithBonus => _bonusEntity.CalcOrderValue(currentValue);

        private bool isSuitMatch => _orders.GroupBy(x => x.card.suit).Count() == 1;

        private bool isValueDown => _communityBattlePtEntity.currentValue >= currentValue;

        public void StockBuff()
        {
            if (currentValue.IsEven())
            {
                _buffEntity.Add(SkillType.Even);
                _buffEntity.Add(SkillType.EvenAtk);
                _buffEntity.Add(SkillType.EvenDef);
            }
            else
            {
                _buffEntity.Add(SkillType.Odd);
                _buffEntity.Add(SkillType.OddAtk);
                _buffEntity.Add(SkillType.OddDef);
            }

            if (_numericRepository.IsExistPrimeNumber(currentValue))
            {
                _buffEntity.Add(SkillType.PrimeNumber);
                _buffEntity.Add(SkillType.PrimeNumberAtk);
                _buffEntity.Add(SkillType.PrimeNumberDef);
                _buffEntity.Add(SkillType.PrimeNumberDollar);
                _buffEntity.Add(SkillType.PrimeNumberHeal);
            }
            else
            {
                _buffEntity.Add(SkillType.NotPrimeNumber);
                _buffEntity.Add(SkillType.NotPrimeNumberAtk);
                _buffEntity.Add(SkillType.NotPrimeNumberDef);
            }

            if (_numericRepository.IsExistSameNumbers(currentValue))
            {
                _buffEntity.Add(SkillType.SameNumbers);
                _buffEntity.Add(SkillType.SameNumbersAtk);
                _buffEntity.Add(SkillType.SameNumbersDef);
                _buffEntity.Add(SkillType.SameNumbersDollar);
                _buffEntity.Add(SkillType.SameNumbersHeal);
            }
            else
            {
                _buffEntity.Add(SkillType.NotSameNumbers);
                _buffEntity.Add(SkillType.NotSameNumbersAtk);
                _buffEntity.Add(SkillType.NotSameNumbersDef);
            }

            if (isSuitMatch)
            {
                _buffEntity.Add(SkillType.SuitMatch);
                _buffEntity.Add(SkillType.SuitMatchAtk);
                _buffEntity.Add(SkillType.SuitMatchDef);

                var suitMatchSkill = _orders[0].card.suit switch
                {
                    Suit.Club => SkillType.SuitMatchClub,
                    Suit.Diamond => SkillType.SuitMatchDiamond,
                    Suit.Heart => SkillType.SuitMatchHeart,
                    Suit.Spade => SkillType.SuitMatchSpade,
                    _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SUIT),
                };
                _buffEntity.Add(suitMatchSkill);
            }
            else
            {
                _buffEntity.Add(SkillType.SuitUnmatch);
                _buffEntity.Add(SkillType.SuitUnmatchAtk);
                _buffEntity.Add(SkillType.SuitUnmatchDef);
            }

            if (isValueDown)
            {
                _buffEntity.Add(SkillType.ValueDownDollar);
                _buffEntity.Add(SkillType.ValueDownHeal);
            }
            else
            {
                _buffEntity.Add(SkillType.ValueUpAtk);
                _buffEntity.Add(SkillType.ValueUpDef);
            }
        }

        public async UniTask PushValueAsync(CancellationToken token)
        {
            _bonusEntity.Clear();

            foreach (var bonus in _numericRepository.Finds(currentValue)) _bonusEntity.Add(bonus);
            if (isSuitMatch) _bonusEntity.Add(_numericRepository.Find(BonusType.SuitMatch));
            if (isValueDown) _bonusEntity.Add(_numericRepository.Find(BonusType.ValueDown));

            await Router.Default.PublishAsync(new OrderValueVO(currentValue, _bonusEntity.ToVO()), token);
            await UniTaskHelper.DelayAsync(1.0f, token);

            _communityBattlePtEntity.Set(currentValueWithBonus);
        }

        public async UniTask PublishCommunityBattlePtAsync(CancellationToken token)
        {
            var orderValue = new OrderValueVO(_communityBattlePtEntity.currentValue, _bonusEntity.ToVO());
            await Router.Default.PublishAsync(orderValue, token);
        }

        public async UniTask ShowOrderCardsAsync(float duration, CancellationToken token)
        {
            await FadeOrderCardsAsync(Fade.Out, duration, token);
        }

        public async UniTask HideOrderCardsAsync(float duration, CancellationToken token)
        {
            await FadeOrderCardsAsync(Fade.In, duration, token);
        }

        private async UniTask FadeOrderCardsAsync(Fade fade, float duration, CancellationToken token)
        {
            await Router.Default.PublishAsync(new OrderCardsFadeVO(fade, duration), token);
        }
    }
}