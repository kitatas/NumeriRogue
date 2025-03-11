using PrimeMillionaire.Top.Domain.UseCase;
using PrimeMillionaire.Top.Presentation.View;
using VContainer.Unity;

namespace PrimeMillionaire.Top.Presentation.Presenter
{
    public sealed class LicensePresenter : IInitializable
    {
        private readonly LicenseUseCase _licenseUseCase;
        private readonly LicenseView _licenseView;

        public LicensePresenter(LicenseUseCase licenseUseCase, LicenseView licenseView)
        {
            _licenseUseCase = licenseUseCase;
            _licenseView = licenseView;
        }

        public void Initialize()
        {
            _licenseView.Render(_licenseUseCase.BuildContents());
        }
    }
}