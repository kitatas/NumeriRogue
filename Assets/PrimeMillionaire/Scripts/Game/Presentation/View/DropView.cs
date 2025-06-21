using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class DropView : MonoBehaviour
    {
        [SerializeField] private Transform gemPoint = default;
        [SerializeField] private GemView gemView = default;

        public void Drop(Transform target, int value, float duration)
        {
            for (int i = 0; i < value; i++)
            {
                var pos = target.position + new Vector3(0.0f, -1.2f);
                var gem = Instantiate(gemView, pos, Quaternion.identity);
                gem.Generate(gemPoint, duration);
                Destroy(gem.gameObject, 3.0f);
            }
        }
    }
}