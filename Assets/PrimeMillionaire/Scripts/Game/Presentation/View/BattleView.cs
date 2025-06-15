using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Utility;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class BattleView : MonoBehaviour
    {
        [SerializeField] private Transform player = default;
        [SerializeField] private Transform enemy = default;
        [SerializeField] private StageView stageView = default;
        [SerializeField] private EntryFxView playerEntryFxView = default;
        [SerializeField] private EntryFxView enemyEntryFxView = default;
        [SerializeField] private DamageFxView playerDamageFxView = default;
        [SerializeField] private DamageFxView enemyDamageFxView = default;

        private CharacterView _playerView;
        private CharacterView _enemyView;

        public async UniTask CreatePlayerAsync(CharacterVO character, CancellationToken token)
        {
            var playerObj = await ResourceHelper.LoadAsync<GameObject>(character.objPath, token);

            playerEntryFxView.Entry(() =>
            {
                _playerView = Instantiate(playerObj, player.position, Quaternion.identity).GetComponent<CharacterView>();
                _playerView.FlipX(Side.Player);
            });
        }

        public async UniTask RenderStageAsync(StageVO stage, CancellationToken token)
        {
            await stageView.LoadAsync(stage, token);
        }

        public async UniTask CreateEnemyAsync(CharacterVO character, CancellationToken token)
        {
            var enemyObj = await ResourceHelper.LoadAsync<GameObject>(character.objPath, token);

            enemyEntryFxView.Entry(() =>
            {
                _enemyView = Instantiate(enemyObj, enemy.position, Quaternion.identity).GetComponent<CharacterView>();
                _enemyView.FlipX(Side.Enemy);
            });
        }

        public async UniTask PlayAttackAnimAsync(Side attacker, CancellationToken token)
        {
            var (attackerView, defenderView) = GetCharacterViews(attacker);
            await attackerView.TweenPositionX(0.0f, CharacterConfig.MOVE_TIME)
                .WithCancellation(token);

            attackerView.Attack(true);
            await UniTaskHelper.DelayAsync(attackerView.applyDamageTime, token);

            attackerView.Attack(false);
        }

        public async UniTask PlayDamageAnimAsync(Side attacker, bool isDestroy, CancellationToken token)
        {
            var (attackerView, defenderView) = GetCharacterViews(attacker);

            defenderView.Damage(true);
            GetDamageFx(attacker).Play();

            this.Delay(defenderView.deadTime, () =>
            {
                defenderView.Damage(false);
                defenderView.Dead(isDestroy);
            });
            await UniTaskHelper.DelayAsync(1.5f, token);

            var x = GetTransform(attacker).localPosition.x;
            await attackerView.TweenPositionX(x, CharacterConfig.MOVE_TIME)
                .WithCancellation(token);
        }

        private (CharacterView attackerView, CharacterView defenderView) GetCharacterViews(Side attacker)
        {
            return attacker switch
            {
                Side.Player => (attackerView: _playerView, defenderView: _enemyView),
                Side.Enemy => (attackerView: _enemyView, defenderView: _playerView),
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }

        private Transform GetTransform(Side side)
        {
            return side switch
            {
                Side.Player => player,
                Side.Enemy => enemy,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }

        private DamageFxView GetDamageFx(Side side)
        {
            return side switch
            {
                Side.Player => playerDamageFxView,
                Side.Enemy => enemyDamageFxView,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }

        public async UniTask DestroyEnemyAsync(CancellationToken token)
        {
            enemyEntryFxView.Exit(() =>
            {
                Destroy(_enemyView.gameObject);
            });
            await UniTaskHelper.DelayAsync(2.0f, token);
        }

        public void PlayBuff(BuffVO buff)
        {
            var fx = Instantiate(buff.fxObject, GetTransform(buff.side));
            Destroy(fx, 3.0f);
        }
    }
}