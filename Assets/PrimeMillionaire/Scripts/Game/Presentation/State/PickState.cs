using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class PickState : BaseState
    {
        private readonly HoldSkillUseCase _holdSkillUseCase;
        private readonly PickSkillUseCase _pickSkillUseCase;

        public PickState(HoldSkillUseCase holdSkillUseCase, PickSkillUseCase pickSkillUseCase)
        {
            _holdSkillUseCase = holdSkillUseCase;
            _pickSkillUseCase = pickSkillUseCase;
        }

        public override GameState state => GameState.Pick;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await (
                _holdSkillUseCase.ApplyViewAsync(token),
                _pickSkillUseCase.ActivateModalAsync(false, token)
            );
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _pickSkillUseCase.LotAsync(token);

            return GameState.SetUp;
        }
    }
}