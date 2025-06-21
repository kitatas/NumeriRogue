using DG.Tweening;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class GemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer body = default;

        public void Generate(Transform target, float duration)
        {
            var r = Random.Range(0.0f, 1.0f) * Mathf.PI;
            var dir = new Vector3(Mathf.Cos(r), Mathf.Sin(r));

            DOTween.Sequence()
                .Append(transform
                    .DOMove(transform.position + dir, duration)
                    .SetEase(Ease.OutCirc))
                .Append(transform
                    .DOMoveY(transform.position.y, duration)
                    .SetEase(Ease.InCirc))
                .AppendInterval(duration * 2.75f)
                .Append(transform
                    .DOMove(target.position, duration)
                    .SetEase(Ease.Linear))
                .SetLink(gameObject);
        }
    }
}