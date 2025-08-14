using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
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

        private int GetCurrentPt(Side side) => GetBattlePtEntity(side).currentValue;

        public async UniTask AddBattlePtAsync(Side side, int value, CancellationToken token)
        {
            GetBattlePtEntity(side).Add(value);
            await PublishAsync(side, token);
        }

        private async UniTask ResetBattlePtAsync(Side side, CancellationToken token)
        {
            GetBattlePtEntity(side).Reset();
            await PublishAsync(side, token);
        }

        public async UniTask ResetAsync(CancellationToken token)
        {
            await UniTask.WhenAll(
                HandConfig.ALL_SIDE.Select(x => ResetBattlePtAsync(x, token))
            );
        }

        private UniTask PublishAsync(Side side, CancellationToken token)
        {
            return Router.Default.PublishAsync(new BattlePtVO(side, GetCurrentPt(side)), token).AsUniTask();
        }

        public Side GetAttacker()
        {
            return GetCurrentPt(Side.Player) >= GetCurrentPt(Side.Enemy) ? Side.Player : Side.Enemy;
        }

        private BaseBattlePtEntity GetBattlePtEntity(Side side)
        {
            return side switch
            {
                Side.Player => _playerBattlePtEntity,
                Side.Enemy => _enemyBattlePtEntity,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }
    }
}