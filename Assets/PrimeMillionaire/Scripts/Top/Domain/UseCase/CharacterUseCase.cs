using System;
using System.Collections.Generic;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.Entity;
using PrimeMillionaire.Common.Domain.Repository;
using R3;

namespace PrimeMillionaire.Top.Domain.UseCase
{
    public sealed class CharacterUseCase : IDisposable
    {
        private readonly PlayerCharacterEntity _playerCharacterEntity;
        private readonly CharacterRepository _characterRepository;
        private readonly DeckRepository _deckRepository;
        private readonly SaveRepository _saveRepository;
        private readonly Subject<OrderCharacterVO> _orderCharacter;

        public CharacterUseCase(PlayerCharacterEntity playerCharacterEntity, CharacterRepository characterRepository,
            DeckRepository deckRepository, SaveRepository saveRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _characterRepository = characterRepository;
            _deckRepository = deckRepository;
            _saveRepository = saveRepository;
            _orderCharacter = new Subject<OrderCharacterVO>();
        }

        public Observable<OrderCharacterVO> orderCharacter => _orderCharacter;

        public List<CharacterVO> GetAll()
        {
            if (_saveRepository.TryLoadProgress(out var progress))
            {
                return _characterRepository.GetReleased(progress);
            }
            else
            {
                throw new Exception();
            }
        }

        public void Order(CharacterType type)
        {
            _playerCharacterEntity.SetType(type);

            var character = _characterRepository.Find(_playerCharacterEntity.type);
            var deck = _deckRepository.GetCards(_playerCharacterEntity.type);
            var order = new OrderCharacterVO(character, deck);
            _orderCharacter?.OnNext(order);
        }

        public void Dispose()
        {
            _orderCharacter?.Dispose();
        }
    }
}