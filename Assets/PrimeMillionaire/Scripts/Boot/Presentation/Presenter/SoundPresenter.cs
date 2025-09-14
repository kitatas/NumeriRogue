using PrimeMillionaire.Common.Domain.UseCase;
using PrimeMillionaire.Common.Presentation.View.Button;
using UnityEngine;
using VContainer.Unity;

namespace PrimeMillionaire.Boot.Presentation.Presenter
{
    public sealed class SoundPresenter : IStartable
    {
        private readonly SoundUseCase _soundUseCase;

        public SoundPresenter(SoundUseCase soundUseCase)
        {
            _soundUseCase = soundUseCase;
        }

        public void Start()
        {
            foreach (var buttonView in Object.FindObjectsByType<BaseButtonView>(FindObjectsSortMode.None))
            {
                buttonView.SetUp(_soundUseCase.Play);
            }
        }
    }
}