using System.Collections.Generic;
using System.IO;
using FastEnumUtility;
using MessagePack;
using MessagePack.Resolvers;
using PrimeMillionaire.Common;
using PrimeMillionaire.Common.Data.DataStore;
using PrimeMillionaire.Game;
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
            databaseBuilder.Append(GetDropRateMaster());
            databaseBuilder.Append(GetParameterMaster());
            databaseBuilder.Append(GetPrimeNumberMaster());
            databaseBuilder.Append(GetSkillMaster());
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

        private static List<DropRateMaster> GetDropRateMaster()
        {
            return new List<DropRateMaster>
            {
                new(1, 2.0f),
                new(2, 1.75f),
                new(3, 1.5f),
                new(4, 1.25f),
                new(5, 1.0f),
            };
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

        private static List<SkillMaster> GetSkillMaster()
        {
            return new List<SkillMaster>
            {
                new(1, 1, 1, 10, 20),
                new(2, 1, 2, 10, 20),
                new(3, 1, 3, 10, 20),
                new(4, 2, 1, 20, 30),
                new(5, 2, 2, 20, 30),
                new(6, 2, 3, 20, 30),
                new(7, 3, 1, 30, 40),
                new(8, 3, 2, 30, 40),
                new(9, 3, 3, 30, 40),
            };
        }
    }
}