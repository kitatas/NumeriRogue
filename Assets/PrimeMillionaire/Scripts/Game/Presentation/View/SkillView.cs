using System.Threading;
using Cysharp.Text;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Presentation.View.Button;
using PrimeMillionaire.Common.Utility;
using R3;
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
        [SerializeField] private CommonButtonView button = default;
        [SerializeField] private TextMeshProUGUI buttonText = default;

        public SkillVO skill { get; private set; }

        public async UniTask RenderAsync(SkillVO value, bool isInteractable, CancellationToken token)
        {
            skill = value;
            background.color = new Color(0.5f, 1.0f, 0.5f);
            icon.color = new Color(1.0f, 1.0f, 1.0f);
            description.text = skill.description;
            button.gameObject.SetActive(true);
            button.SetInteractable(isInteractable);
            buttonText.text = skill.isHold ? "Trash" : ZString.Format("${0}", skill.price);

            icon.sprite = await ResourceHelper.LoadAsync<Sprite>(skill.skillBase.iconPath, token);
        }

        public async UniTask RenderEmptyAsync(CancellationToken token)
        {
            background.color = new Color(0.5f, 0.5f, 0.5f);
            icon.color = new Color(0.2f, 0.2f, 0.2f);
            icon.sprite = null;
            description.text = "---";
            button.gameObject.SetActive(false);
            buttonText.text = "";

            icon.sprite = null;
            await UniTask.Yield(token);
        }

        public Observable<SkillVO> OnClickAsObservable()
        {
            return button.push.Select(_ => skill);
        }

        public void Repaint(int value)
        {
            if (skill == null) return;

            button.SetInteractable(value >= skill.price && buttonText.text != "SoldOut");
        }

        public void SoldOut()
        {
            buttonText.text = "SoldOut";
        }
    }
}