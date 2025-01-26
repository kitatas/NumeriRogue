using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class SkillView : MonoBehaviour
    {
        [SerializeField] private Image icon = default;
        [SerializeField] private TextMeshProUGUI description = default;

        public async UniTask RenderAsync(SkillVO skill, CancellationToken token)
        {
            description.text = skill.description;

            // WANT: skill icon
            await UniTask.Yield(token);
        }
    }
}