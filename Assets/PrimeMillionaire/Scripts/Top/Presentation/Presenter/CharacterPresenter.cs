using PrimeMillionaire.Common;
using PrimeMillionaire.Top.Presentation.View;
using R3;
using VContainer.Unity;
using VitalRouter;

namespace PrimeMillionaire.Top.Presentation.Presenter
{
    public sealed class CharacterPresenter : IStartable
    {
        private readonly CharacterListView _characterListView;

        public CharacterPresenter(CharacterListView characterListView)
        {
            _characterListView = characterListView;
        }

        public void Start()
        {
            Router.Default
                .SubscribeAwait<CharacterVO>(async (x, context) =>
                {
                    await _characterListView.RenderAsync(x, context.CancellationToken);
                })
                .AddTo(_characterListView);
        }
    }
}