using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
using UniEx;
using UnityEngine;

namespace PrimeMillionaire.Game.Presentation.View
{
    public sealed class BattleView : MonoBehaviour
    {
        [SerializeField] private Transform player = default;
        [SerializeField] private Transform enemy = default;

        private CharacterView _playerView;
        private CharacterView _enemyView;

        public async UniTask CreatePlayerAsync(CharacterVO character, CancellationToken token)
        {
            var playerObj = await ResourceHelper.LoadAsync<GameObject>(character.objPath, token);
            _playerView = Instantiate(playerObj, player.position, Quaternion.identity).GetComponent<CharacterView>();
            _playerView.FlipX(Side.Player);
        }

        public async UniTask CreateEnemyAsync(CharacterVO character, CancellationToken token)
        {
            var enemyObj = await ResourceHelper.LoadAsync<GameObject>(character.objPath, token);
            _enemyView = Instantiate(enemyObj, enemy.position, Quaternion.identity).GetComponent<CharacterView>();
            _enemyView.FlipX(Side.Enemy);
        }

        public void PlayAnimation(Side attacker, bool isDestroy)
        {
            var (attackerView, defenderView) = GetCharacterViews(attacker);

            attackerView.Attack(true);

            this.Delay(attackerView.applyDamageTime, () =>
            {
                attackerView.Attack(false);
                defenderView.Damage(true);

                this.Delay(defenderView.deadTime, () =>
                {
                    defenderView.Damage(false);
                    defenderView.Dead(isDestroy);
                });
            });
        }

        private (CharacterView attackerView, CharacterView defenderView) GetCharacterViews(Side attacker)
        {
            return attacker switch
            {
                Side.Player => (attackerView: _playerView, defenderView: _enemyView),
                Side.Enemy => (attackerView: _enemyView, defenderView: _playerView),
                _ => throw new Exception(),
            };
        }
    }
}