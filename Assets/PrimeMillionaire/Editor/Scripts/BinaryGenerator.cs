using System.Collections.Generic;
using System.IO;
using MessagePack;
using MessagePack.Resolvers;
using Newtonsoft.Json;
using PrimeMillionaire.Common.Data.DataStore;
using UnityEditor;

namespace PrimeMillionaire.Editor.Scripts
{
    public static class BinaryGenerator
    {
        [MenuItem("Tools/MasterMemory/CreateBinary")]
        private static void GenerateCardMaster()
        {
            StaticCompositeResolver.Instance.Register(
                MasterMemoryResolver.Instance,
                GeneratedResolver.Instance,
                StandardResolver.Instance
            );

            var options = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
            MessagePackSerializer.DefaultOptions = options;

            var databaseBuilder = new DatabaseBuilder();
            databaseBuilder.Append(DeserializeJson<CardMaster>("card"));
            databaseBuilder.Append(DeserializeJson<CharacterMaster>("character"));
            databaseBuilder.Append(DeserializeJson<CharacterStageMaster>("character_stage"));
            databaseBuilder.Append(DeserializeJson<DeckMaster>("deck"));
            databaseBuilder.Append(DeserializeJson<DropRateMaster>("drop_rate"));
            databaseBuilder.Append(DeserializeJson<LevelMaster>("level"));
            databaseBuilder.Append(DeserializeJson<NumericBonusMaster>("numeric_bonus"));
            databaseBuilder.Append(DeserializeJson<NumericMaster>("numeric"));
            databaseBuilder.Append(DeserializeJson<SkillBaseMaster>("skill_base"));
            databaseBuilder.Append(DeserializeJson<SkillEffectMaster>("skill_effect"));
            databaseBuilder.Append(DeserializeJson<SkillTargetMaster>("skill_target"));
            var binary = databaseBuilder.Build();

            var bytes = "Assets/Externals/Binary/MasterMemory.bytes";
            var directory = Path.GetDirectoryName(bytes);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllBytes(bytes, binary);
            AssetDatabase.Refresh();
        }

        private static List<T> DeserializeJson<T>(string fileName)
        {
            var json = File.ReadAllText($"Master/Jsons/{fileName}.json");
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}