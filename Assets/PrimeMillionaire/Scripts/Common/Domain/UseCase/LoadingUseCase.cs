using System;
using PrimeMillionaire.Common.Data.Entity;
using R3;

namespace PrimeMillionaire.Common.Domain.UseCase
{
    public sealed class LoadingUseCase : IDisposable
    {
        private readonly LoadingEntity _loadingEntity;
        private readonly ReactiveProperty<bool> _isLoad;

        public LoadingUseCase(LoadingEntity loadingEntity)
        {
            _loadingEntity = loadingEntity;
            _isLoad = new ReactiveProperty<bool>(_loadingEntity.value);
        }

        public Observable<bool> isLoad => _isLoad;

        public bool isLoading => _isLoad.Value;

        public void Set(bool value)
        {
            _loadingEntity.Set(value);
            _isLoad.Value = _loadingEntity.value;
        }

        public void Dispose()
        {
            _isLoad?.Dispose();
        }
    }
}