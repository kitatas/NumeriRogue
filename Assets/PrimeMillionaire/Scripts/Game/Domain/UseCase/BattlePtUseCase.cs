using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Data.Entity;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class BattlePtUseCase
    {
        private readonly PlayerBattlePtEntity _playerBattlePtEntity;
        private readonly EnemyBattlePtEntity _enemyBattlePtEntity;

        public BattlePtUseCase(PlayerBattlePtEntity playerBattlePtEntity, EnemyBattlePtEntity enemyBattlePtEntity)
        {
            _playerBattlePtEntity = playerBattlePtEntity;
            _enemyBattlePtEntity = enemyBattlePtEntity;
        }

        public int currentPlayerPt => _playerBattlePtEntity.currentValue;
        public int currentEnemyPt => _enemyBattlePtEntity.currentValue;

        public async UniTask AddPlayerBattlePtAsync(int value, CancellationToken token)
        {
            _playerBattlePtEntity.Add(value);
            await Router.Default.PublishAsync(new BattlePtVO(Side.Player, currentPlayerPt), token);
        }

        public async UniTask AddEnemyBattlePtAsync(int value, CancellationToken token)
        {
            _enemyBattlePtEntity.Add(value);
            await Router.Default.PublishAsync(new BattlePtVO(Side.Enemy, currentEnemyPt), token);
        }

        public async UniTask ResetAsync(CancellationToken token)
        {
            _playerBattlePtEntity.Reset();
            _enemyBattlePtEntity.Reset();

            await (
                Router.Default.PublishAsync(new BattlePtVO(Side.Player, currentPlayerPt), token).AsUniTask(),
                Router.Default.PublishAsync(new BattlePtVO(Side.Enemy, currentEnemyPt), token).AsUniTask()
            );
        }

        public Side GetAttacker()
        {
            return currentPlayerPt >= currentEnemyPt ? Side.Player : Side.Enemy;
        }
    }
}