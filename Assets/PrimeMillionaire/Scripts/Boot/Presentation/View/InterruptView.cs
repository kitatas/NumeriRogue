using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Presentation.View.Button;
using R3;
using UnityEngine;

namespace PrimeMillionaire.Boot.Presentation.View
{
    public sealed class InterruptView : MonoBehaviour
    {
        [SerializeField] private CommonButtonView decision = default;
        [SerializeField] private CommonButtonView cancel = default;

        public Observable<ButtonType> pressDecision => decision.push.Select(_ => ButtonType.Decision);
        public Observable<ButtonType> pressCancel => cancel.push.Select(_ => ButtonType.Cancel);
    }
}