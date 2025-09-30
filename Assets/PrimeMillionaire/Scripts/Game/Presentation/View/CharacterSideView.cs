using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class CharacterSideView : MonoBehaviour
    {
        [SerializeField] private Transform init = default;
        [SerializeField] private EntryFxView entryFxView = default;
        [SerializeField] private DamageFxView damageFxView = default;

        public CharacterView characterView { get; private set; }
        public Vector3 position => init.position;

        public async UniTask CreateCharacterAsync(Side side, CharacterVO character, CancellationToken token)
        {
            var obj = await ResourceHelper.LoadAsync<GameObject>(character.objPath, token);

            entryFxView.Entry(() =>
            {
                characterView = Instantiate(obj, position, Quaternion.identity).GetComponent<CharacterView>();
                characterView.FlipX(side);
            });
        }

        public async UniTask PlayAttackAnimAsync(Side side, CancellationToken token)
        {
            await characterView.TweenPositionX(1.0f * side.ToSign(), CharacterConfig.MOVE_TIME)
                .WithCancellation(token);

            characterView.Attack(true);
            await UniTaskHelper.DelayAsync(characterView.applyDamageTime, token);

            characterView.Attack(false);
        }

        public async UniTask PlayHitOrDeathAnimAsync(bool isDeath, Action<CharacterView> drop, CancellationToken token)
        {
            damageFxView.Play();

            if (isDeath)
            {
                characterView.Death(true);
                drop?.Invoke(characterView);
            }
            else
            {
                characterView.Hit(true);
                this.DelayFrame(1, () => characterView.Hit(false));
            }

            await UniTaskHelper.DelayAsync(1.5f, token);
        }

        public async UniTask TweenInitPositionAsync(CancellationToken token)
        {
            await characterView.TweenPositionX(init.localPosition.x, CharacterConfig.MOVE_TIME)
                .WithCancellation(token);
        }

        public async UniTask DestroyCharacterAsync(CancellationToken token)
        {
            entryFxView.Exit(() => Destroy(characterView.gameObject));
            await UniTaskHelper.DelayAsync(2.0f, token);
        }

        public void PlayBuffFx(BuffVO buff)
        {
            var fx = Instantiate(buff.fxObject, init);
            Destroy(fx, 3.0f);
        }
    }
}