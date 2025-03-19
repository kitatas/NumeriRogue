using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Presentation.View.Button;
using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Common.Presentation.View.Modal
{
    public sealed class ExceptionModalView : BaseModalView
    {
        [SerializeField] private TextMeshProUGUI message = default;
        [SerializeField] private CommonButtonView button = default;

        public void Render(string value)
        {
            message.text = value;
        }

        public async UniTask ShowAndClickAsync(float duration, CancellationToken token)
        {
            await ShowAsync(duration, token);
            await button.OnClickAsync(token);
        }
    }
}