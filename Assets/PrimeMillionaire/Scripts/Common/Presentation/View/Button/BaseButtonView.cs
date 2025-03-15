using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PrimeMillionaire.Common.Presentation.View.Button
{
    public abstract class BaseButtonView : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button button = default;

        public Observable<PointerEventData> push => button.OnPointerDownAsObservable();
        public UniTask OnClickAsync(CancellationToken token) => button.OnClickAsync(token);
    }
}