using System.Collections.Generic;
using System.Linq;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Data.Entity;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class HandUseCase
    {
        private readonly DeckEntity _deckEntity;
        private readonly SortEntity _sortEntity;
        private readonly PlayerHandEntity _playerHandEntity;
        private readonly EnemyHandEntity _enemyHandEntity;

        public HandUseCase(DeckEntity deckEntity, SortEntity sortEntity, PlayerHandEntity playerHandEntity,
            EnemyHandEntity enemyHandEntity)
        {
            _deckEntity = deckEntity;
            _sortEntity = sortEntity;
            _playerHandEntity = playerHandEntity;
            _enemyHandEntity = enemyHandEntity;
        }

        public void SwitchSort()
        {
            _sortEntity.Switch();
        }

        public List<HandVO> GetHands(Side side)
        {
            var hands = GetHandEntity(side).hands
                .Select((v, i) => new HandVO(v, i, _deckEntity.GetCard(v)));

            return _sortEntity.value switch
            {
                Sort.Rank => hands.OrderBy(x => x.card.rank).ThenBy(x => x.card.suit).ToList(),
                Sort.Suit => hands.OrderBy(x => x.card.suit).ThenBy(x => x.card.rank).ToList(),
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SORT)
            };
        }

        public CardVO GetCard(Side side, int index)
        {
            return _deckEntity.GetCard(GetHands(side)[index].deckIndex);
        }

        public void RemoveCards(Side side, IEnumerable<int> index)
        {
            var hands = GetHands(side);
            var removeIndex = index
                .Select(x => hands[x].handIndex)
                .OrderByDescending(x => x);

            foreach (var i in removeIndex)
            {
                GetHandEntity(side).Remove(i);
            }
        }

        private BaseHandEntity GetHandEntity(Side side)
        {
            return side switch
            {
                Side.Player => _playerHandEntity,
                Side.Enemy => _enemyHandEntity,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }

        public bool IsPlayerHandsEmpty()
        {
            return _playerHandEntity.hands.Count == 0;
        }
    }
}