using PrimeMillionaire.Top.Domain.UseCase;
using PrimeMillionaire.Top.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Top.Presentation.Presenter
{
    public sealed class CharacterPresenter : IStartable
    {
        private readonly CharacterUseCase _characterUseCase;
        private readonly CharacterScrollView _characterScrollView;
        private readonly OrderCharacterView _orderCharacterView;

        public CharacterPresenter(CharacterUseCase characterUseCase, CharacterScrollView characterScrollView,
            OrderCharacterView orderCharacterView)
        {
            _characterUseCase = characterUseCase;
            _characterScrollView = characterScrollView;
            _orderCharacterView = orderCharacterView;
        }

        public void Start()
        {
            _characterScrollView.Init(_characterUseCase.GetAll(), _characterUseCase.Order);

            _characterUseCase.orderCharacter
                .SubscribeAwait(async (x, token) =>
                {
                    await _orderCharacterView.RenderAsync(x, token);
                })
                .AddTo(_orderCharacterView);
        }
    }
}