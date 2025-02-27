namespace PrimeMillionaire.Common.Data.DataStore
{
    public sealed class SaveData
    {
        public string uid;

        public static SaveData Create()
        {
            return new SaveData
            {
                uid = "",
            };
        }

        public bool IsEmptyUid()
        {
            return string.IsNullOrEmpty(uid);
        }
    }
}