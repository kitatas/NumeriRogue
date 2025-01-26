using System.Threading;
using Cysharp.Threading.Tasks;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class HoldSkillUseCase
    {
        public async UniTask AddAsync(SkillVO skill, CancellationToken token)
        {
            UnityEngine.Debug.Log($"type: {skill.type}, value: {skill.value}");
            await UniTask.Yield(token);
        }
    }
}