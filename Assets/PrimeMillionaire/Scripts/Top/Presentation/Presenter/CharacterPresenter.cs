using PrimeMillionaire.Top.Domain.UseCase;
using PrimeMillionaire.Top.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Top.Presentation.Presenter
{
    public sealed class CharacterPresenter : IPostStartable
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

        public void PostStart()
        {
            _characterUseCase.orderCharacter
                .SubscribeAwait(async (x, token) =>
                {
                    await _orderCharacterView.RenderAsync(x, token);
                })
                .AddTo(_orderCharacterView);

            var (characters, index) = _characterUseCase.GetAllAndIndex();
            _characterScrollView.Init(characters, index, _characterUseCase.Order);
        }
    }
}