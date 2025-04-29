using PrimeMillionaire.Common.Presentation.View.Button;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class GiveUpView : MonoBehaviour
    {
        [SerializeField] private CommonButtonView decision = default;
        [SerializeField] private CommonButtonView backTitle = default;

        public Observable<PointerEventData> giveUp => decision.push;
        public Observable<PointerEventData> back => backTitle.push;
    }
}