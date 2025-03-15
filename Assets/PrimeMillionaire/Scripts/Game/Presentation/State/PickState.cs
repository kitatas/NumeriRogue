using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Game.Domain.UseCase;

namespace PrimeMillionaire.Game.Presentation.State
{
    public sealed class PickState : BaseState
    {
        private readonly HoldSkillUseCase _holdSkillUseCase;
        private readonly ModalUseCase _modalUseCase;
        private readonly PickSkillUseCase _pickSkillUseCase;

        public PickState(HoldSkillUseCase holdSkillUseCase, ModalUseCase modalUseCase,
            PickSkillUseCase pickSkillUseCase)
        {
            _holdSkillUseCase = holdSkillUseCase;
            _modalUseCase = modalUseCase;
            _pickSkillUseCase = pickSkillUseCase;
        }

        public override GameState state => GameState.Pick;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await (
                _holdSkillUseCase.ApplyViewAsync(token)
            );
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _pickSkillUseCase.LotAsync(token);
            await _modalUseCase.ShowAsync(ModalType.PickSkill, token);
            await _modalUseCase.HideAsync(ModalType.PickSkill, token);

            return GameState.SetUp;
        }
    }
}