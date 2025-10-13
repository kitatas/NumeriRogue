using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.View.Button;
using UnityEngine;
using VContainer.Unity;

namespace PrimeMillionaire.Boot.Presentation.Presenter
{
    public sealed class SoundPresenter : IPostInitializable
    {
        private readonly ISoundUseCase _soundUseCase;

        public SoundPresenter(ISoundUseCase soundUseCase)
        {
            _soundUseCase = soundUseCase;
        }

        public void PostInitialize()
        {
            foreach (var buttonView in Object.FindObjectsByType<BaseButtonView>(FindObjectsSortMode.None))
            {
                buttonView.SetUp(_soundUseCase.Play);
            }
        }
    }
}