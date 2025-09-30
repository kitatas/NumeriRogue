using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class BattleView : MonoBehaviour
    {
        [SerializeField] private CharacterSideView playerSideView = default;
        [SerializeField] private CharacterSideView enemySideView = default;
        [SerializeField] private StageView stageView = default;
        [SerializeField] private DropView dropView = default;

        public async UniTask RenderStageAsync(StageVO stage, CancellationToken token)
        {
            await stageView.LoadAsync(stage, token);
        }

        public async UniTask CreateCharacterAsync(Side side, CharacterVO character, CancellationToken token)
        {
            await GetSideView(side).CreateCharacterAsync(side, character, token);
        }

        public async UniTask PlayAttackAnimAsync(Side side, CancellationToken token)
        {
            await GetSideView(side).PlayAttackAnimAsync(side, token);
        }

        public async UniTask PlayDamageAnimAsync(Side side, bool isDeath, CancellationToken token)
        {
            await GetSideView(side).PlayHitOrDeathAnimAsync(isDeath, x =>
            {
                if (side is Side.Enemy)
                {
                    this.Delay(x.deadTime, () => dropView.Drop(x.transform, 10, 0.25f));
                }
            }, token);

            var attacker = side.ToOppositeSide();
            await GetSideView(attacker).TweenInitPositionAsync(token);
        }

        public async UniTask DestroyCharacterAsync(Side side, CancellationToken token)
        {
            await GetSideView(side).DestroyCharacterAsync(token);
        }

        public void PlayBuff(BuffVO buff)
        {
            GetSideView(buff.side).PlayBuffFx(buff);
        }

        private CharacterSideView GetSideView(Side side)
        {
            return side switch
            {
                Side.Player => playerSideView,
                Side.Enemy => enemySideView,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }
    }
}