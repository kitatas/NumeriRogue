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

        private CharacterView _playerView;
        private CharacterView _enemyView;

        public async UniTask CreatePlayerAsync(CharacterVO character, CancellationToken token)
        {
            var playerObj = await ResourceHelper.LoadAsync<GameObject>(character.objPath, token);
            _playerView = Instantiate(playerObj, player.position, Quaternion.identity).GetComponent<CharacterView>();
            _playerView.FlipX(Side.Player);

            await stageView.LoadAsync(character.stage, token);
        }

        public async UniTask CreateEnemyAsync(CharacterVO character, CancellationToken token)
        {
            var enemyObj = await ResourceHelper.LoadAsync<GameObject>(character.objPath, token);
            _enemyView = Instantiate(enemyObj, enemy.position, Quaternion.identity).GetComponent<CharacterView>();
            _enemyView.FlipX(Side.Enemy);
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

            this.Delay(defenderView.deadTime, () =>
            {
                defenderView.Damage(false);
                defenderView.Dead(isDestroy);
            });
            await UniTaskHelper.DelayAsync(1.5f, token);

            var x = GetDefaultPositionX(attacker);
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

        private float GetDefaultPositionX(Side side)
        {
            return side switch
            {
                Side.Player => player.localPosition.x,
                Side.Enemy => enemy.localPosition.x,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }

        public void DestroyEnemy()
        {
            Destroy(_enemyView.gameObject);
        }

        public void PlayBuff(BuffVO buff)
        {
            var parent = buff.side switch
            {
                Side.Player => player,
                Side.Enemy => enemy,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE)
            };

            var fx = Instantiate(buff.fxObject, parent);
            Destroy(fx, 3.0f);
        }
    }
}