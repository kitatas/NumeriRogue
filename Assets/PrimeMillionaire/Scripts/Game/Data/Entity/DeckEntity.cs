using System;
using System.Collections.Generic;
using PrimeMillionaire.Common;
using Random = UnityEngine.Random;

namespace PrimeMillionaire.Game.Data.Entity
{
    public sealed class DeckEntity
    {
        private readonly List<CardVO> _cards;
        private int _index;

        public DeckEntity()
        {
            _cards = new List<CardVO>(CardConfig.MAX_RANK * CardConfig.SUITS.Length);
        }

        public void Init(IEnumerable<CardVO> cards)
        {
            var c = DebugConfig.IS_FORCE_WIN switch
            {
                Side.None => cards,
                Side.Player => new List<CardVO>
                    { new(1, 13), new(1, 1), new(2, 13), new(2, 1), new(3, 13), new(3, 1), },
                Side.Enemy => new List<CardVO>
                    { new(1, 1), new(1, 13), new(2, 1), new(2, 13), new(3, 1), new(3, 13), },
                _ => throw new Exception()
            };

            _cards.AddRange(c);
        }

        public void Init(DeckVO deck)
        {
            _cards.AddRange(deck.cards);
        }

        public void Refresh()
        {
            _index = 0;
            if (DebugConfig.IS_FORCE_WIN == Side.None)
            {
                Shuffle();
            }
        }

        /// <summary>
        /// Fisher-Yates shuffle
        /// </summary>
        private void Shuffle()
        {
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                var j = Random.Range(0, i + 1);
                (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
            }
        }

        /// <summary>
        /// Deckのindexを返す
        /// </summary>
        /// <returns></returns>
        public int Draw() => _index++;

        public CardVO GetCard(int index) => _cards[index];

        public DeckVO ToVO() => new(_cards);
    }
}