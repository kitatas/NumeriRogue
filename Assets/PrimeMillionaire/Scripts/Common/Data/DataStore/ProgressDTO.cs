using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PrimeMillionaire.Common.Data.DataStore
{
    [Serializable]
    public sealed class ProgressDTO
    {
        public List<CharacterProgressDTO> characterStages;

        public ProgressDTO()
        {
            characterStages = new List<CharacterProgressDTO>
            {
                new(new CharacterProgressVO(0, ProgressStatus.Clear)),
            };
        }

        public ProgressDTO(ProgressVO progress)
        {
            characterStages = progress.characterProgress.Select(x => new CharacterProgressDTO(x)).ToList();
        }

        public ProgressVO ToVO() => new(characterStages.Select(x => x.ToVO()).ToList());
        public string ToJson() => JsonConvert.SerializeObject(this);
    }

    [Serializable]
    public sealed class CharacterProgressDTO
    {
        public int id;
        public ProgressStatus status;

        public CharacterProgressDTO()
        {
        }

        public CharacterProgressDTO(CharacterProgressVO characterProgress)
        {
            id = characterProgress.id;
            status = characterProgress.status;
        }

        public CharacterProgressVO ToVO() => new(id, status);
    }
}