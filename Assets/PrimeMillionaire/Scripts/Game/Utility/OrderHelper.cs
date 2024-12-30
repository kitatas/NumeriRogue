using System.Collections.Generic;
using System.Linq;

namespace PrimeMillionaire.Game.Utility
{
    public static class OrderHelper
    {
        public static (int[] index, int value) GetOrder(IEnumerable<HandVO> hands, int value)
        {
            var cards = hands
                .Select(x => x.card)
                .ToList();

            var orders = new List<(int[] index, int value)>();
            for (int i = 0; i < cards.Count - 1; i++)
            {
                for (int j = 0; j < cards.Count - 1; j++)
                {
                    if (j == i) continue;

                    for (int k = 0; k < cards.Count - 1; k++)
                    {
                        if (k == i || k == j) continue;

                        orders.Add((new[] { i, j, k },
                            int.Parse($"{cards[i].rank}{cards[j].rank}{cards[k].rank}")));
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