using PrimeMillionaire.Common.Data.DataStore;
using UnityEngine;

namespace PrimeMillionaire.Common.Domain.Repository
{
    public sealed class SaveRepository
    {
        public SaveData Load()
        {
            var data = ES3.Load(SaveConfig.ES3_KEY, defaultValue: "");
            return string.IsNullOrEmpty(data)
                ? CreateNewData()
                : JsonUtility.FromJson<SaveData>(data);
        }

        public bool TryLoadProgress(out ProgressVO progress)
        {
            var data = Load();
            progress = data.progress.ToVO();
            return data.HasProgress();
        }

        public bool TryLoadInterrupt(out InterruptVO interrupt)
        {
            var data = Load();
            interrupt = data.interrupt.ToVO();
            return data.HasInterrupt();
        }

        private SaveData CreateNewData()
        {
            var data = SaveData.Create();
            Save(data);
            return data;
        }

        public void Save(SaveData value)
        {
            ES3.Save(SaveConfig.ES3_KEY, JsonUtility.ToJson(value));
        }

        public void Save(InterruptVO interrupt)
        {
            var data = Load();
            data.interrupt = interrupt == null ? null : new InterruptDTO(interrupt);
            Save(data);
        }

        public void Save(ProgressVO progress)
        {
            var data = Load();
            data.progress = new ProgressDTO(progress);
            Save(data);
        }

        public void SaveMaster(VolumeVO master)
        {
            var data = Load();
            data.sound.master = new VolumeDTO(master);
            Save(data);
        }

        public void SaveBgm(VolumeVO bgm)
        {
            var data = Load();
            data.sound.bgm = new VolumeDTO(bgm);
            Save(data);
        }

        public void SaveSe(VolumeVO se)
        {
            var data = Load();
            data.sound.se = new VolumeDTO(se);
            Save(data);
        }

        public void DeleteInterrupt()
        {
            Save(interrupt: null);
        }
    }
}