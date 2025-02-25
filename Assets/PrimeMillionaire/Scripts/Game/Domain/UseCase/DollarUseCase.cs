using System;
using PrimeMillionaire.Game.Data.Entity;
using R3;

namespace PrimeMillionaire.Game.Domain.UseCase
{
    public sealed class DollarUseCase : IDisposable
    {
        private readonly DollarEntity _dollarEntity;
        private readonly ReactiveProperty<int> _dollar;

        public DollarUseCase(DollarEntity dollarEntity)
        {
            _dollarEntity = dollarEntity;
            _dollar = new ReactiveProperty<int>(_dollarEntity.currentValue);
        }

        public Observable<int> dollar => _dollar;
        public int currentValue => _dollar.Value;

        // TODO: クリア条件の仮値
        public bool IsClear() => _dollarEntity.IsEnough(1500);
        public bool IsConsume(int value) => _dollarEntity.IsEnough(value);

        public void Add(int value)
        {
            _dollarEntity.Add(value);
            _dollar.Value = _dollarEntity.currentValue;
        }

        public void Consume(int value)
        {
            _dollarEntity.Subtract(value);
            _dollar.Value = _dollarEntity.currentValue;
        }

        public void Dispose()
        {
            _dollar?.Dispose();
        }
    }
}