using PrimeMillionaire.Common.Presentation.View.Button;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class DeleteView : MonoBehaviour
    {
        [SerializeField] private CommonButtonView decision = default;
        [SerializeField] private CommonButtonView backTitle = default;

        public Observable<PointerEventData> delete => decision.push;
        public Observable<PointerEventData> back => backTitle.push;
    }
}