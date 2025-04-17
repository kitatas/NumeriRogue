using System.Collections.Generic;
using PrimeMillionaire.Common.Utility;

namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class SaveData
    {
        public string uid;
        public ProgressVO progress;
        public InterruptDTO interrupt;

        public static SaveData Create()
        {
            return new SaveData
            {
                uid = "",
                progress = new ProgressVO(new List<CharacterProgressVO>
                {
                    new(0.ToCharacterType(), ProgressStatus.Clear),
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
            if (interrupt.player.type == CharacterType.None) return false;
            return true;
        }
    }
}