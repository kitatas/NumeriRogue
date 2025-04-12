using System.Collections.Generic;
using System.Linq;
using Cysharp.Text;

namespace PrimeMillionaire.Game.Utility
{
    public static class OrderHelper
    {
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