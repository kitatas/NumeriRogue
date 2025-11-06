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
        private readonly UserEntity _userEntity;
        private readonly CharacterRepository _characterRepository;
        private readonly CharacterStageRepository _characterStageRepository;
        private readonly Subject<OrderCharacterVO> _orderCharacter;

        public CharacterUseCase(PlayerCharacterEntity playerCharacterEntity, UserEntity userEntity,
            CharacterRepository characterRepository, CharacterStageRepository characterStageRepository)
        {
            _playerCharacterEntity = playerCharacterEntity;
            _userEntity = userEntity;
            _characterRepository = characterRepository;
            _characterStageRepository = characterStageRepository;
            _orderCharacter = new Subject<OrderCharacterVO>();
        }

        public Observable<OrderCharacterVO> orderCharacter => _orderCharacter;

        private List<StageCharacterVO> GetAll()
        {
            var progress = _userEntity.progress;
            return _characterStageRepository.GetReleased(progress)
                .Select(x =>
                {
                    var character = _characterRepository.Find(x);
                    var characterProgress = progress.Find(x) ?? new CharacterProgressVO(x, ProgressStatus.New);
                    return new StageCharacterVO(character, characterProgress);
                })
                .ToList();
        }

        public (List<StageCharacterVO>, int) GetAllAndIndex()
        {
            var characters = GetAll();
            var id = _playerCharacterEntity.id == 0
                ? characters.OrderBy(x => x.character.parameter.id).Last().character.parameter.id
                : _playerCharacterEntity.id;

            // 初期選択されているキャラ
            Order(id);

            var index = characters.FindIndex(x => x.character.parameter.id == id);
            return (characters, index);
        }

        public void Order(int id)
        {
            _playerCharacterEntity.SetType(id);

            var character = _characterRepository.Find(_playerCharacterEntity.id);
            var deck = _characterStageRepository.GetDeck(_playerCharacterEntity.id);
            var order = new OrderCharacterVO(character, deck);
            _orderCharacter?.OnNext(order);
        }

        public void Dispose()
        {
            _orderCharacter?.Dispose();
        }
    }
}