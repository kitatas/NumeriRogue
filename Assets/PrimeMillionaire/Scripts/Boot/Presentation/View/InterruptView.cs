using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Presentation.View.Button;
using UnityEngine;

namespace PrimeMillionaire.Boot.Presentation.View
{
    public sealed class InterruptView : MonoBehaviour
    {
        [SerializeField] private CommonButtonView decision = default;
        [SerializeField] private CommonButtonView cancel = default;

        public async UniTask<ButtonType> PushAnyAsync(CancellationToken token)
        {
            var buttonTasks = new List<UniTask<ButtonType>>
            {
                DecisionAsync(token),
                CancelAsync(token),
            };
            var (_, button) = await UniTask.WhenAny(buttonTasks);

            return button;
        }

        private async UniTask<ButtonType> DecisionAsync(CancellationToken token)
        {
            await decision.OnClickAsync(token);
            return ButtonType.Decision;
        }

        private async UniTask<ButtonType> CancelAsync(CancellationToken token)
        {
            await cancel.OnClickAsync(token);
            return ButtonType.Cancel;
        }
    }
}