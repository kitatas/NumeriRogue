namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class SaveData
    {
        public string uid;
        public InterruptVO interrupt;

        public static SaveData Create()
        {
            return new SaveData
            {
                uid = "",
                interrupt = null,
            };
        }

        public bool IsEmptyUid()
        {
            return string.IsNullOrEmpty(uid);
        }
    }
}