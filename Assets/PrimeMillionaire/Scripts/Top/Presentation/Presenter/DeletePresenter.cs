using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Top.Domain.UseCase;
using PrimeMillionaire.Top.Presentation.View;
using R3;
using VContainer.Unity;

namespace PrimeMillionaire.Top.Presentation.Presenter
{
    public sealed class DeletePresenter : IStartable
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly ModalUseCase _modalUseCase;
        private readonly UserDataUseCase _userDataUseCase;
        private readonly DeleteView _deleteView;

        public DeletePresenter(SceneUseCase sceneUseCase, ModalUseCase modalUseCase, UserDataUseCase userDataUseCase,
            DeleteView deleteView)
        {
            _sceneUseCase = sceneUseCase;
            _modalUseCase = modalUseCase;
            _userDataUseCase = userDataUseCase;
            _deleteView = deleteView;
        }

        public void Start()
        {
            _deleteView.delete
                .SubscribeAwait(async (_, token) =>
                {
                    _userDataUseCase.Delete();
                    await _modalUseCase.ShowAsync(ModalType.DeleteComplete, token);
                })
                .AddTo(_deleteView);

            _deleteView.back
                .Subscribe(_ => _sceneUseCase.Load(SceneName.Boot, LoadType.Fade))
                .AddTo(_deleteView);
        }
    }
}