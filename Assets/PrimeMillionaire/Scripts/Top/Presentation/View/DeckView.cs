using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Presentation.View;
using UnityEngine;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class DeckView : MonoBehaviour
    {
        [SerializeField] private CardView[] clubs = default;
        [SerializeField] private CardView[] diamonds = default;
        [SerializeField] private CardView[] hearts = default;
        [SerializeField] private CardView[] spades = default;

        public void Render(DeckVO deck)
        {
            Refresh();

            foreach (var card in deck.cards)
            {
                var cardViews = card.suit switch
                {
                    Suit.Club => clubs,
                    Suit.Diamond => diamonds,
                    Suit.Heart => hearts,
                    Suit.Spade => spades,
                    _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SUIT),
                };
                cardViews[card.rank - 1].ActivateMask(false);
            }
        }

        private void Refresh()
        {
            foreach (var club in clubs) club.ActivateMask(true);
            foreach (var diamond in diamonds) diamond.ActivateMask(true);
            foreach (var heart in hearts) heart.ActivateMask(true);
            foreach (var spade in spades) spade.ActivateMask(true);
        }
    }
}