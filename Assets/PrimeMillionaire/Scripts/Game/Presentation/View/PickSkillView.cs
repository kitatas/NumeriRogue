using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class PickSkillView : MonoBehaviour
    {
        [SerializeField] private SkillView[] skillViews = default;

        public SkillView[] skills => skillViews;

        public void Repaint(int value)
        {
            foreach (var skillView in skillViews)
            {
                skillView.Repaint(value);
            }
        }

        public async UniTask RenderAsync(PickSkillVO pickSkill, bool isConsume, CancellationToken token)
        {
            await skillViews[pickSkill.index].RenderAsync(pickSkill.skill, isConsume, token);
        }
    }
}