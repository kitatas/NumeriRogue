using System;
using R3;

namespace PrimeMillionaire.Common.Domain.UseCase
{
    public sealed class LoadingUseCase : IDisposable
    {
        private readonly ReactiveProperty<bool> _isLoad;

        public LoadingUseCase()
        {
            _isLoad = new ReactiveProperty<bool>(false);
        }

        public Observable<bool> isLoad => _isLoad;

        public void Set(bool value)
        {
            _isLoad.Value = value;
        }

        public void Dispose()
        {
            _isLoad?.Dispose();
        }
    }
}