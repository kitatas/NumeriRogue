using PrimeMillionaire.Common.Data.DataStore;
using UnityEngine;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class SaveRepository
    {
        private static SaveDTO ParseLoad()
        {
            var data = ES3.Load(SaveConfig.ES3_KEY, defaultValue: "");
            return string.IsNullOrEmpty(data)
                ? CreateNewData()
                : JsonUtility.FromJson<SaveDTO>(data);
        }

        public SaveVO Load()
        {
            return ParseLoad().ToVO();
        }

        public bool TryLoadInterrupt(out InterruptVO interrupt)
        {
            var data = Load();
            interrupt = data.interrupt;
            return data.hasInterrupt;
        }

        private static SaveDTO CreateNewData()
        {
            var data = new SaveDTO();
            Save(data);
            return data;
        }

        private static void Save(SaveDTO value)
        {
            ES3.Save(SaveConfig.ES3_KEY, JsonUtility.ToJson(value));
        }

        public void Save(UserVO user)
        {
            var data = ParseLoad();
            data.uid = user.uid;
            Save(data);
        }

        public void Save(InterruptVO interrupt)
        {
            var data = ParseLoad();
            data.interrupt = interrupt == null ? null : new InterruptDTO(interrupt);
            Save(data);
        }

        public void SaveMaster(VolumeVO master)
        {
            var data = ParseLoad();
            data.sound.master = new VolumeDTO(master);
            Save(data);
        }

        public void SaveBgm(VolumeVO bgm)
        {
            var data = ParseLoad();
            data.sound.bgm = new VolumeDTO(bgm);
            Save(data);
        }

        public void SaveSe(VolumeVO se)
        {
            var data = ParseLoad();
            data.sound.se = new VolumeDTO(se);
            Save(data);
        }

        public void Delete()
        {
            var data = new SaveDTO(ParseLoad().sound);
            Save(data);
        }

        public void DeleteInterrupt()
        {
            Save(interrupt: null);
        }
    }
}