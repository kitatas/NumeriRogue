using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using R3;
using VitalRouter;

namespace PrimeMillionaire.Top.Domain.UseCase
{
    public sealed class CharacterUseCase : IDisposable
    {
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly CharacterRepository _characterRepository;
        private readonly Subject<CharacterVO> _orderCharacter;

        public CharacterUseCase(PlayerCharacterEntity playerCharacterEntity, CharacterRepository characterRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _characterRepository = characterRepository;
            _orderCharacter = new Subject<CharacterVO>();
        }

        public Observable<CharacterVO> orderCharacter => _orderCharacter;

        public async UniTask RenderPlayableCharacterAsync(CancellationToken token)
        {
            var characters = _characterRepository.GetAll();
            foreach (var character in characters)
            {
                await Router.Default.PublishAsync(character, token).AsUniTask();
            }

            Order(characters[0].type);
        }

        public void Order(CharacterType type)
        {
            _playerCharacterEntity.SetType(type);

            var character = _characterRepository.Find(_playerCharacterEntity.type);
            _orderCharacter?.OnNext(character);
        }

        public void Dispose()
        {
            _orderCharacter?.Dispose();
        }
    }
}