using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePack;
using MessagePack.Resolvers;
using Newtonsoft.Json;
using PrimeMillionaire.Common.Data.DataStore;
using PrimeMillionaire.Common.Utility;
using UnityEditor;
using UnityEngine;

namespace PrimeMillionaire.Editor.Scripts
{
    public static class BinaryGenerator
    {
        [MenuItem("Tools/MasterMemory/CreateBinary")]
        private static async void GenerateCardMaster()
        {
            var messagePackResolvers = CompositeResolver.Create(
                MasterMemoryResolver.Instance,
                GeneratedResolver.Instance,
                StandardResolver.Instance
            );

            var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
            MessagePackSerializer.DefaultOptions = options;

            var token = CancellationToken.None;
            var card = await GetCardMasterAsync(token);
            var character = await GetCharacterMasterAsync(token);
            var dropRate = await GetDropRateMasterAsync(token);
            var parameter = await GetParameterMasterAsync(token);
            var skill = await GetSkillMasterAsync(token);
            var level = await GetLevelMasterAsync(token);

            var databaseBuilder = new DatabaseBuilder();
            databaseBuilder.Append(card);
            databaseBuilder.Append(character);
            databaseBuilder.Append(dropRate);
            databaseBuilder.Append(parameter);
            databaseBuilder.Append(GetPrimeNumberMaster());
            databaseBuilder.Append(skill);
            databaseBuilder.Append(level);
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

        private static async UniTask<List<CardMaster>> GetCardMasterAsync(CancellationToken token)
        {
            var json = await ResourceHelper.LoadExternalsAsync<TextAsset>("Jsons/card.json", token);
            return JsonConvert.DeserializeObject<List<CardMaster>>(json.text);
        }

        private static async UniTask<List<CharacterMaster>> GetCharacterMasterAsync(CancellationToken token)
        {
            var json = await ResourceHelper.LoadExternalsAsync<TextAsset>("Jsons/character.json", token);
            return JsonConvert.DeserializeObject<List<CharacterMaster>>(json.text);
        }

        private static async UniTask<List<DropRateMaster>> GetDropRateMasterAsync(CancellationToken token)
        {
            var json = await ResourceHelper.LoadExternalsAsync<TextAsset>("Jsons/drop_rate.json", token);
            return JsonConvert.DeserializeObject<List<DropRateMaster>>(json.text);
        }

        private static async UniTask<List<ParameterMaster>> GetParameterMasterAsync(CancellationToken token)
        {
            var json = await ResourceHelper.LoadExternalsAsync<TextAsset>("Jsons/parameter.json", token);
            return JsonConvert.DeserializeObject<List<ParameterMaster>>(json.text);
        }

        private static List<PrimeNumberMaster> GetPrimeNumberMaster()
        {
            var primeNumberMaster = new List<PrimeNumberMaster>();
            for (int i = 111; i < 131313; i += 2)
            {
                if (IsPrime(i)) primeNumberMaster.Add(new PrimeNumberMaster(i));
            }

            return primeNumberMaster;
        }

        private static bool IsPrime(int value)
        {
            for (int i = 3; i * i <= value; i += 2)
            {
                if (value % i == 0) return false;
            }

            return true;
        }

        private static async UniTask<List<SkillMaster>> GetSkillMasterAsync(CancellationToken token)
        {
            var json = await ResourceHelper.LoadExternalsAsync<TextAsset>("Jsons/skill.json", token);
            return JsonConvert.DeserializeObject<List<SkillMaster>>(json.text);
        }

        private static async UniTask<List<LevelMaster>> GetLevelMasterAsync(CancellationToken token)
        {
            var json = await ResourceHelper.LoadExternalsAsync<TextAsset>("Jsons/level.json", token);
            return JsonConvert.DeserializeObject<List<LevelMaster>>(json.text);
        }
    }
}