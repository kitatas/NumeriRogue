using Cysharp.Text;
using TMPro;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class EnemyCountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI enemyCountValue = default;

        public void Render(int value)
        {
            enemyCountValue.text = ZString.Format("{0}", value);
        }
    }
}