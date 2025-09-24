using PrimeMillionaire.Game.Presentation.View.Button;
using R3;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class SortView : MonoBehaviour
    {
        [SerializeField] private SortButtonView rankSortButtonView = default;
        [SerializeField] private SortButtonView suitSortButtonView = default;

        public Observable<Sort> pushAny => rankSortButtonView.pushSort
            .Merge(suitSortButtonView.pushSort);

        public void SwitchBackground(Sort value)
        {
            rankSortButtonView.SwitchBackground(value);
            suitSortButtonView.SwitchBackground(value);
        }

        public void SetInteractable(bool value)
        {
            rankSortButtonView.SetInteractable(value);
            suitSortButtonView.SetInteractable(value);
        }
    }
}