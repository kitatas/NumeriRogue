using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class OrderView : MonoBehaviour
    {
        [SerializeField] private Button button = default;

        public Observable<PointerEventData> push => button.OnPointerDownAsObservable();
    }
}