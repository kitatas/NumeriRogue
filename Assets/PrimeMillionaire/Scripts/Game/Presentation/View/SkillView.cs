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
            background.color = new Color(0.5f, 1.0f, 0.5f);
            icon.color = new Color(1.0f, 1.0f, 1.0f);
            description.text = skill.description;

            // WANT: skill icon
            await UniTask.Yield(token);
        }

        public async UniTask RenderEmptyAsync(CancellationToken token)
        {
            background.color = new Color(0.5f, 0.5f, 0.5f);
            icon.color = new Color(0.2f, 0.2f, 0.2f);
            description.text = "---";

            icon.sprite = null;
            await UniTask.Yield(token);
        }

        public async UniTask<SkillVO> SelectAsync(CancellationToken token)
        {
            await background.GetAsyncPointerDownTrigger().OnPointerDownAsync(token);
            return skill;
        }
    }
}