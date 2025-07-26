using System.Collections.Generic;
using System.Linq;
using Cysharp.Text;
using PrimeMillionaire.Common;

namespace PrimeMillionaire.Game.Utility
{
    public static class OrderHelper
    {
        private static readonly int[] _qka = { 1, 12, 13 };

        public static PokerHands GetPokerHands(IEnumerable<OrderVO> orders)
        {
            var cards = orders.Select(o => o.card).ToArray();

            var ranks = cards.Select(c => c.rank).OrderBy(r => r).ToArray();
            var rankCount = ranks.Distinct().Count();
            var isStraight = (rankCount == 3 && ranks[2] - ranks[0] == 2) || ranks.SequenceEqual(_qka);

            var suits = cards.Select(c => c.suit);
            var suitCount = suits.Distinct().Count();
            var isFlush = suitCount == 1;

            if (isStraight && isFlush) return PokerHands.StraightFlush;
            if (rankCount == 1) return PokerHands.ThreeOfAKind;
            if (isStraight) return PokerHands.Straight;
            if (isFlush) return PokerHands.Flush;
            if (rankCount == 2) return PokerHands.OnePair;

            return PokerHands.HighCard;
        }

        public static (int[] index, int value) GetOrder(IEnumerable<HandVO> hands, int value)
        {
            var cards = hands
                .Select(x => x.card.rank)
                .ToArray();

            var orders = new List<(int[] index, int value)>();
            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cards.Length; j++)
                {
                    if (j == i) continue;

                    for (int k = 0; k < cards.Length; k++)
                    {
                        if (k == i || k == j) continue;

                        orders.Add((new[] { i, j, k }, int.Parse(ZString.Concat(cards[i], cards[j], cards[k]))));
                    }
                }
            }

            var sortingOrders = orders
                .OrderBy(x => x.value)
                .ToList();

            var result = sortingOrders
                .FirstOrDefault(x => x.value > value);

            return result == default ? sortingOrders[0] : result;
        }
    }
}