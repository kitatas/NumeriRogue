using PrimeMillionaire.Common;
using PrimeMillionaire.Top.Domain.UseCase;
using PrimeMillionaire.Top.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Top.Presentation.Presenter
{
    public sealed class CharacterPresenter : IStartable
    {
        private readonly CharacterUseCase _characterUseCase;
        private readonly CharacterListView _characterListView;

        public CharacterPresenter(CharacterUseCase characterUseCase, CharacterListView characterListView)
        {
            _characterUseCase = characterUseCase;
            _characterListView = characterListView;
        }

        public void Start()
        {
            Router.Default
                .SubscribeAwait<CharacterVO>(async (x, context) =>
                {
                    var characterView = await _characterListView.RenderAsync(x, context.CancellationToken);
                    characterView.pointerDown
                        .Select(_ => x.type)
                        .Subscribe(_characterUseCase.Order)
                        .AddTo(_characterListView);
                })
                .AddTo(_characterListView);

            _characterUseCase.orderCharacter
                .SubscribeAwait(async (x, token) =>
                {
                    await _characterListView.OrderAsync(x, token);
                })
                .AddTo(_characterListView);
        }
    }
}