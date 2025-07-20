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

        public bool IsConsume(int value) => _dollarEntity.IsEnough(value);

        public void Update()
        {
            _dollar.Value = _dollarEntity.currentValue;
        }

        public void Add(int value)
        {
            _dollarEntity.Add(value);
            Update();
        }

        public void Consume(int value)
        {
            _dollarEntity.Subtract(value);
            Update();
        }

        public void Dispose()
        {
            _dollar?.Dispose();
        }
    }
}