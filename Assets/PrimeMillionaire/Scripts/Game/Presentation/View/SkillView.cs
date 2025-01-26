using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class SkillView : MonoBehaviour
    {
        [SerializeField] private Image background = default;
        [SerializeField] private Image icon = default;
        [SerializeField] private TextMeshProUGUI description = default;

        public SkillVO skill { get; private set; }

        public async UniTask RenderAsync(SkillVO value, CancellationToken token)
        {
            skill = value;
            description.text = skill.description;

            // WANT: skill icon
            await UniTask.Yield(token);
        }

        public async UniTask<SkillVO> SelectAsync(CancellationToken token)
        {
            await background.GetAsyncPointerDownTrigger().OnPointerDownAsync(token);
            return skill;
        }
    }
}