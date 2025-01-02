using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common.Utility;
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
    }
}