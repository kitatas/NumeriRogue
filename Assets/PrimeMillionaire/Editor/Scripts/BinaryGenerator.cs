using System.Collections.Generic;
using System.IO;
using FastEnumUtility;
using MessagePack;
using MessagePack.Resolvers;
using PrimeMillionaire.Game;
using PrimeMillionaire.Game.Data.DataStore;
using UnityEditor;

namespace PrimeMillionaire.Editor.Scripts
{
    public static class BinaryGenerator
    {
        [MenuItem("Tools/MasterMemory/CreateBinary")]
        private static void GenerateCardMaster()
        {
            var messagePackResolvers = CompositeResolver.Create(
                MasterMemoryResolver.Instance,
                GeneratedResolver.Instance,
                StandardResolver.Instance
            );

            var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
            MessagePackSerializer.DefaultOptions = options;

            var databaseBuilder = new DatabaseBuilder();
            databaseBuilder.Append(GetCardMaster());
            databaseBuilder.Append(GetCharacterMaster());
            databaseBuilder.Append(GetParameterMaster());
            databaseBuilder.Append(GetPrimeNumberMaster());
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

        private static List<CardMaster> GetCardMaster()
        {
            var cardMaster = new List<CardMaster>();
            var id = 1;
            foreach (var suit in CardConfig.SUITS)
            {
                for (int i = 1; i <= CardConfig.MAX_RANK; i++)
                {
                    cardMaster.Add(new CardMaster(id++, suit.ToInt32(), i));
                }
            }

            return cardMaster;
        }

        private static List<CharacterMaster> GetCharacterMaster()
        {
            var characterMaster = new List<CharacterMaster>();
            foreach (var type in FastEnum.GetValues<CharacterType>())
            {
                if (type == CharacterType.None) continue;
                characterMaster.Add(new CharacterMaster(type.ToInt32()));
            }

            return characterMaster;
        }

        private static List<ParameterMaster> GetParameterMaster()
        {
            var parameterMaster = new List<ParameterMaster>();
            parameterMaster.Add(new ParameterMaster(CharacterType.Andromeda.ToInt32(), 1100, 110, 240));
            parameterMaster.Add(new ParameterMaster(CharacterType.Borealjuggernaut.ToInt32(), 1200, 130, 280));
            return parameterMaster;
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
    }
}