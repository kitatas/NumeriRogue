using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Presentation.View.Button;
using R3;
using UnityEngine;

namespace PrimeMillionaire.Top.Presentation.View.Button
{
    public sealed class OtherAppButtonView : BaseButtonView
    {
        private void Start()
        {
#if UNITY_WEBGL
            SetInteractable(false);   
#else
            push.Subscribe(_ => Application.OpenURL(UrlConfig.DEVELOPER_APP_URL))
                .AddTo(this);
#endif
        }
    }
}