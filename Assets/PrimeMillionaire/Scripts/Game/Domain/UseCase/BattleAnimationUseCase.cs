using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Game.Data.Entity;
using VitalRouter;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class BattleAnimationUseCase
    {
        private readonly EnemyParameterEntity _enemyParameterEntity;
        private readonly PlayerParameterEntity _playerParameterEntity;

        public BattleAnimationUseCase(EnemyParameterEntity enemyParameterEntity,
            PlayerParameterEntity playerParameterEntity)
        {
            _enemyParameterEntity = enemyParameterEntity;
            _playerParameterEntity = playerParameterEntity;
        }

        public async UniTask EntryAsync(Side side, CharacterVO character, CancellationToken token)
        {
            await PlayAnimationAsync(side, character, token);
        }

        public async UniTask ExitAsync(Side side, CancellationToken token)
        {
            await PlayAnimationAsync(side, BattleAnim.Exit, token);
        }

        public async UniTask AttackAsync(Side attacker, CancellationToken token)
        {
            await PlayAnimationAsync(attacker, BattleAnim.Attack, token);
        }

        public async UniTask DamageOrDeadAsync(Side side, CancellationToken token)
        {
            var battleAnim = IsDeath(side) ? BattleAnim.Death : BattleAnim.Hit;
            await PlayAnimationAsync(side, battleAnim, token);
        }

        public bool IsDeath(Side side)
        {
            return side switch
            {
                Side.Player => _playerParameterEntity.isDead,
                Side.Enemy => _enemyParameterEntity.isDead,
                _ => throw new QuitExceptionVO(ExceptionConfig.NOT_FOUND_SIDE),
            };
        }

        private static async UniTask PlayAnimationAsync(Side side, CharacterVO character, CancellationToken token)
        {
            await PlayAnimationAsync(new BattleAnimationVO(side, character), token);
        }

        private static async UniTask PlayAnimationAsync(Side side, BattleAnim battleAnim, CancellationToken token)
        {
            await PlayAnimationAsync(new BattleAnimationVO(side, battleAnim), token);
        }

        private static async UniTask PlayAnimationAsync(BattleAnimationVO battleAnimation, CancellationToken token)
        {
            await Router.Default.PublishAsync(battleAnimation, token);
        }
    }
}