using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PrimeMillionaire.Editor.Scripts
{
    public sealed class DuelystCharacterGenerator : EditorWindow
    {
        private Texture2D _tex;
        private TextAsset _xml;

        [MenuItem("Tools/" + nameof(DuelystCharacterGenerator))]
        public static void ShowWindow()
        {
            GetWindow<DuelystCharacterGenerator>(nameof(DuelystCharacterGenerator));
        }

        private void OnGUI()
        {
            _tex = (Texture2D)EditorGUILayout.ObjectField("Texture", _tex, typeof(Texture2D), false);
            _xml = (TextAsset)EditorGUILayout.ObjectField("XML", _xml, typeof(TextAsset), false);

            if (GUILayout.Button(nameof(Exec)))
            {
                Exec();
            }
        }

        private void Exec()
        {
            Debug.Log($"[START] {nameof(DuelystCharacterGenerator)}");

            try
            {
                if (_tex == null || _xml == null) throw new Exception("Set Texture and XML");

                SliceTexture();
                var clipMap = CreateClips();
                var controller = CreateOverrideController(clipMap);
                CreatePrefabVariant(controller);
            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog("ERROR", e.Message, "OK");
            }

            Debug.Log($"[FINISH] {nameof(DuelystCharacterGenerator)}");
        }

        private void SliceTexture()
        {
            var texPath = AssetDatabase.GetAssetPath(_tex);
            var importer = AssetImporter.GetAtPath(texPath) as TextureImporter;
            if (importer == null) throw new Exception("Failed to get texture importer");

            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.filterMode = FilterMode.Point;
            importer.textureCompression = TextureImporterCompression.Uncompressed;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(_xml.text);

            // <key>frames</key> 直後の <dict> を取得
            var dicts = xmlDocument.SelectSingleNode("//dict/key[.='frames']/following-sibling::dict[1]");
            if (dicts == null) throw new Exception("Not found dict nodes");

            var keys = dicts.SelectNodes("key");
            if (keys == null) throw new Exception("Not found key nodes");

            var metas = new List<SpriteMetaData>();
            foreach (XmlNode key in keys)
            {
                // <key> の次に定義されている <dict> を取得
                var dict = key.NextSibling;
                if (dict == null) throw new Exception("Not found dict node");

                // <string> 内の Rect を取得
                var rectString = dict.SelectSingleNode("key[.='frame']/following-sibling::string[1]");
                if (rectString == null) throw new Exception("Not found string rect");

                var parts = rectString.InnerText.Replace("{", "").Replace("}", "").Split(',');
                if (parts.Length != 4) throw new Exception("Failed to parse rect");

                var rect = new Rect(
                    float.Parse(parts[0]),
                    float.Parse(parts[1]),
                    float.Parse(parts[2]),
                    float.Parse(parts[3])
                );
                rect.y = _tex.height - rect.y - rect.height;

                var meta = new SpriteMetaData
                {
                    name = Path.GetFileNameWithoutExtension(key.InnerText),
                    rect = rect,
                    alignment = (int)SpriteAlignment.Center,
                    pivot = new Vector2(0.5f, 0.5f),
                };
                metas.Add(meta);
            }

            // SpriteMetaData の適応と再インポート
            importer.spritesheet = metas.ToArray();
            importer.SaveAndReimport();

            RegisterAddressables<Texture2D>(_tex, "CharaImages");

            Debug.Log($"[SUCCESS] {nameof(SliceTexture)}");
        }

        private Dictionary<string, AnimationClip> CreateClips()
        {
            var texPath = AssetDatabase.GetAssetPath(_tex);
            var sprites = AssetDatabase.LoadAllAssetsAtPath(texPath).OfType<Sprite>()
                .OrderBy(x => x.name)
                .ToArray();

            // NOTE: 生成対象の clip を変更したい場合はここを更新する
            var targetSprites = new List<string> { "attack", "death", "hit", "idle" };

            var spriteMap = new Dictionary<string, List<Sprite>>();
            foreach (var sprite in sprites)
            {
                var nameParts = sprite.name.Split('_');
                if (nameParts.Length < 2) throw new Exception("Invalid sprite name");

                // 対象外の場合
                if (!targetSprites.Contains(nameParts[2])) continue;

                var clipName = string.Join("_", nameParts.Take(nameParts.Length - 1));
                if (!spriteMap.ContainsKey(clipName)) spriteMap[clipName] = new List<Sprite>();
                spriteMap[clipName].Add(sprite);
            }

            // 出力先の folder 生成
            var baseFolder = "Assets/PrimeMillionaire/Animations/Characters";
            var output = Path.Combine(baseFolder, _tex.name);
            if (!AssetDatabase.IsValidFolder(output)) AssetDatabase.CreateFolder(baseFolder, _tex.name);

            // AnimationClip の生成
            var clips = new Dictionary<string, AnimationClip>();
            foreach (var sprite in spriteMap)
            {
                var clip = new AnimationClip
                {
                    frameRate = 12.0f,
                };

                var curveBinding = new EditorCurveBinding
                {
                    type = typeof(SpriteRenderer),
                    path = "",
                    propertyName = "m_Sprite",
                };

                var keyFrames = new ObjectReferenceKeyframe[sprite.Value.Count];
                for (int i = 0; i < sprite.Value.Count; i++)
                {
                    keyFrames[i] = new ObjectReferenceKeyframe
                    {
                        time = i / clip.frameRate,
                        value = sprite.Value[i],
                    };
                }

                AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyFrames);

                var settings = AnimationUtility.GetAnimationClipSettings(clip);

                // idle のみ loop
                if (sprite.Key.ToLower().Contains("idle"))
                {
                    settings.loopTime = true;
                    clip.wrapMode = WrapMode.Loop;
                }

                AnimationUtility.SetAnimationClipSettings(clip, settings);

                var clipPath = Path.Combine(output, $"{sprite.Key}.anim");
                AssetDatabase.CreateAsset(clip, clipPath);

                clips.Add(sprite.Key, clip);
            }

            Debug.Log($"[SUCCESS] {nameof(CreateClips)}");
            return clips;
        }

        private AnimatorOverrideController CreateOverrideController(Dictionary<string, AnimationClip> clipMap)
        {
            var baseFolder = "Assets/PrimeMillionaire/Animations/Characters";
            var controllerPath = Path.Combine(baseFolder, "character_controller.controller");
            var baseController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(controllerPath);
            if (baseController == null) throw new Exception("Not found character_controller controller");

            var overrideController = new AnimatorOverrideController
            {
                runtimeAnimatorController = baseController
            };

            var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            overrideController.GetOverrides(overrides);

            for (int i = 0; i < overrides.Count; i++)
            {
                var keys = overrides[i].Key.name.Split('_');
                if (clipMap.TryGetValue($"{_tex.name}_{keys[2]}", out var clip))
                {
                    overrides[i] = new KeyValuePair<AnimationClip, AnimationClip>(overrides[i].Key, clip);
                }
            }

            overrideController.ApplyOverrides(overrides);

            var output = Path.Combine(baseFolder, _tex.name);
            var overrideControllerPath = Path.Combine(output, $"{_tex.name}.overrideController");
            AssetDatabase.CreateAsset(overrideController, overrideControllerPath);

            Debug.Log($"[SUCCESS] {nameof(CreateOverrideController)}");
            return overrideController;
        }

        private void CreatePrefabVariant(AnimatorOverrideController animatorController)
        {
            var baseFolder = "Assets/PrimeMillionaire/Prefabs/Characters";
            var characterPath = Path.Combine(baseFolder, "Character.Prefab");
            var basePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(characterPath);
            if (basePrefab == null) throw new Exception("Not found Character prefab");

            var instance = (GameObject)PrefabUtility.InstantiatePrefab(basePrefab);
            if (instance == null) throw new Exception("Failed to instantiate Character prefab");

            var character = _tex.name.Split("_")[1];
            var prefabName = $"Character - {character}.prefab";
            instance.name = prefabName;

            // Body の animation controller / sprite を更新
            var body = instance.transform.Find("Body")?.gameObject;
            if (body == null) throw new Exception("Not found body");

            var texPath = AssetDatabase.GetAssetPath(_tex);
            var sprite = AssetDatabase.LoadAllAssetsAtPath(texPath)
                .OfType<Sprite>()
                .First(y => y.name == $"{_tex.name}_idle_000");
            if (sprite == null) throw new Exception("Not found character_idle_000");

            body.GetComponent<Animator>().runtimeAnimatorController = animatorController;
            body.GetComponent<SpriteRenderer>().sprite = sprite;

            var prefabPath = Path.Combine(baseFolder, prefabName);
            var variant = PrefabUtility.SaveAsPrefabAsset(instance, prefabPath, out var isSuccess);
            if (!isSuccess || variant == null) throw new Exception("Failed to save Character prefab");

            AssetDatabase.ImportAsset(prefabPath);
            AssetDatabase.SaveAssets();

            RegisterAddressables<GameObject>(variant, "Characters");

            if (instance != null) DestroyImmediate(instance);

            Debug.Log($"[SUCCESS] {nameof(CreatePrefabVariant)}");
        }

        private void RegisterAddressables<T>(T target, string groupName) where T : Object
        {
            var assetPath = AssetDatabase.GetAssetPath(target);
            var guid = AssetDatabase.AssetPathToGUID(assetPath);

            var settings = AddressableAssetSettingsDefaultObject.Settings;
            var group = settings.FindGroup(groupName);

            var entry = settings.CreateOrMoveEntry(guid, group, readOnly: false, postEvent: true);
            entry.address = assetPath;

            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssets();

            Debug.Log($"[SUCCESS] {nameof(RegisterAddressables)} at {target.name}");
        }
    }
}