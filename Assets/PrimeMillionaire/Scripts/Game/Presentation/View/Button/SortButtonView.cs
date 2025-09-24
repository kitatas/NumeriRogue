using PrimeMillionaire.Common.Presentation.View.Button;
using R3;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View.Button
{
    public sealed class SortButtonView : BaseButtonView
    {
        [SerializeField] private Sort sort = default;

        public Observable<Sort> pushSort => push
            .Select(_ => sort);
    }
}