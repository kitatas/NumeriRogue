using System.Collections.Generic;

namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class SaveData
    {
        public string uid;
        public ProgressVO progress;
        public InterruptVO interrupt;

        public static SaveData Create()
        {
            return new SaveData
            {
                uid = "",
                progress = new ProgressVO(new List<ClearVO>
                {
                    new(characterNo: 0, isClear: true),
                    new(characterNo: 1, isClear: false),
                }),
                interrupt = null,
            };
        }

        public bool IsEmptyUid()
        {
            return string.IsNullOrEmpty(uid);
        }

        public bool HasProgress()
        {
            return progress != null;
        }

        public bool HasInterrupt()
        {
            if (interrupt == null) return false;
            if (interrupt.playerCharacter == CharacterType.None) return false;
            return true;
        }
    }
}