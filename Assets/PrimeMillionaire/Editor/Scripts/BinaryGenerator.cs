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
        [MenuItem("Tools/BinaryGenerate/" + nameof(CardMaster))]
        private static void GenerateCardMaster()
        {
            var messagePackResolvers = CompositeResolver.Create(
                MasterMemoryResolver.Instance,
                GeneratedResolver.Instance,
                StandardResolver.Instance
            );

            var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
            MessagePackSerializer.DefaultOptions = options;

            var cardMaster = new List<CardMaster>();
            var id = 1;
            foreach (var suit in CardConfig.SUITS)
            {
                for (int i = 1; i <= CardConfig.MAX_RANK; i++)
                {
                    var imgPath = $"Assets/Externals/Sprites/Cards/card_{suit.FastToString().ToLower()}s_{i}.png";
                    cardMaster.Add(new CardMaster(id++, suit, i, imgPath));
                }
            }

            var databaseBuilder = new DatabaseBuilder();
            databaseBuilder.Append(cardMaster);
            var binary = databaseBuilder.Build();

            var bytes = "Assets/Externals/Binary/CardMaster.bytes";
            var directory = Path.GetDirectoryName(bytes);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllBytes(bytes, binary);
            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/BinaryGenerate/" + nameof(CharacterMaster))]
        private static void SetUp()
        {
            var messagePackResolvers = CompositeResolver.Create(
                MasterMemoryResolver.Instance,
                GeneratedResolver.Instance,
                StandardResolver.Instance
            );

            var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
            MessagePackSerializer.DefaultOptions = options;

            var characterMaster = new List<CharacterMaster>();
            foreach (var type in FastEnum.GetValues<CharacterType>())
            {
                if (type == CharacterType.None) continue;
                var objPath = $"Assets/PrimeMillionaire/Prefabs/Characters/{type.FastToString()}.prefab";
                characterMaster.Add(new CharacterMaster(type, objPath));
            }

            var databaseBuilder = new DatabaseBuilder();
            databaseBuilder.Append(characterMaster);
            var binary = databaseBuilder.Build();

            var bytes = "Assets/Externals/Binary/CharacterMaster.bytes";
            var directory = Path.GetDirectoryName(bytes);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllBytes(bytes, binary);
            AssetDatabase.Refresh();
        }
    }
}