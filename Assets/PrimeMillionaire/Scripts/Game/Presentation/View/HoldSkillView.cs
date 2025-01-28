using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class HoldSkillView : MonoBehaviour
    {
        [SerializeField] private SkillView[] skillViews = default;

        public async UniTask RenderAsync(HoldSkillVO holdSkill, CancellationToken token)
        {
            for (int i = 0; i < skillViews.Length; i++)
            {
                if (i < holdSkill.skills.Count)
                {
                    await skillViews[i].RenderAsync(holdSkill.skills[i], token);
                }
                else
                {
                    await skillViews[i].RenderEmptyAsync(token);
                }
            }
        }
    }
}