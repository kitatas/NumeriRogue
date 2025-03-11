using System.Text;
using PrimeMillionaire.Top.Domain.Repository;

namespace PrimeMillionaire.Top.Domain.UseCase
{
    public sealed class LicenseUseCase
    {
        private readonly LicenseRepository _licenseRepository;

        public LicenseUseCase(LicenseRepository licenseRepository)
        {
            _licenseRepository = licenseRepository;
        }

        public string BuildContents()
        {
            var stringBuilder = new StringBuilder();
            foreach (var license in _licenseRepository.GetAll())
            {
                stringBuilder.AppendLine($"### {license.title}\n");
                stringBuilder.AppendLine($"{license.content}\n\n");
            }

            return stringBuilder.ToString();
        }
    }
}