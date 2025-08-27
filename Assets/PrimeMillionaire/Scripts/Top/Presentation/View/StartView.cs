using PrimeMillionaire.Common.Presentation.View.Button;
using R3;
using UnityEngine;

namespace PrimeMillionaire.Top.Presentation.View
{
    public sealed class StartView : MonoBehaviour
    {
        [SerializeField] private CommonButtonView startButton = default;

        public Observable<Unit> pressStart => startButton.push.Select(_ => Unit.Default);
    }
}