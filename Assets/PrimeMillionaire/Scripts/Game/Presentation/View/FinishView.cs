using DG.Tweening;
using PrimeMillionaire.Common;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class FinishView : MonoBehaviour
    {
        [SerializeField] private ClearView clearView = default;
        [SerializeField] private FailView failView = default;

        public void Init()
        {
            clearView.Init();
            failView.Init();
        }

        public Tween FadeIn(FinishType type, float duration)
        {
            return type switch
            {
                FinishType.Clear => clearView.FadeIn(duration),
                FinishType.Fail => failView.FadeIn(duration),
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_FINISH),
            };
        }
    }
}