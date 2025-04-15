using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly CharacterStageRepository _characterStageRepository;
        private readonly SaveRepository _saveRepository;
        private readonly Subject<OrderCharacterVO> _orderCharacter;

        public CharacterUseCase(PlayerCharacterEntity playerCharacterEntity, CharacterRepository characterRepository,
            CharacterStageRepository characterStageRepository, SaveRepository saveRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _characterRepository = characterRepository;
            _characterStageRepository = characterStageRepository;
            _saveRepository = saveRepository;
            _orderCharacter = new Subject<OrderCharacterVO>();
        }

        public Observable<OrderCharacterVO> orderCharacter => _orderCharacter;

        public List<StageCharacterVO> GetAll()
        {
            if (_saveRepository.TryLoadProgress(out var progress))
            {
                return _characterRepository.GetReleased(progress)
                    .Select(character =>
                    {
                        var characterProgress = progress.Find(character.type) ??
                                                new CharacterProgressVO(character.type, ProgressStatus.New);
                        return new StageCharacterVO(character, characterProgress);
                    })
                    .ToList();
            }
            else
            {
                throw new RebootExceptionVO(ExceptionConfig.FAILED_LOAD_PROGRESS);
            }
        }

        public void Order(CharacterType type)
        {
            _playerCharacterEntity.SetType(type);

            var character = _characterRepository.Find(_playerCharacterEntity.type);
            var deck = _characterStageRepository.GetCards(_playerCharacterEntity.type);
            var order = new OrderCharacterVO(character, deck);
            _orderCharacter?.OnNext(order);
        }

        public void Dispose()
        {
            _orderCharacter?.Dispose();
        }
    }
}