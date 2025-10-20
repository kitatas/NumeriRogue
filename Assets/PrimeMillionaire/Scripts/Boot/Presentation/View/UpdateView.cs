using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Presentation.View.Button;
using R3;
using UnityEngine;

namespace PrimeMillionaire.Boot.Presentation.View
{
    public sealed class UpdateView : MonoBehaviour
    {
        [SerializeField] private CommonButtonView decision = default;

        private void Start()
        {
            decision.push
                .Subscribe(_ => Application.OpenURL(UrlConfig.APP_URL))
                .AddTo(this);
        }
    }
}