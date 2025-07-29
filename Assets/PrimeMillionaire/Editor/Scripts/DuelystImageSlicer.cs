using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace PrimeMillionaire.Editor.Scripts
{
    public sealed class DuelystImageSlicer : EditorWindow
    {
        private TextAsset _xml;
        private Texture2D _texture;

        [MenuItem("Tools/" + nameof(DuelystImageSlicer))]
        public static void ShowWindow()
        {
            GetWindow<DuelystImageSlicer>("Plist Slicer");
        }

        private void OnGUI()
        {
            _xml = (TextAsset)EditorGUILayout.ObjectField("XML (plist)", _xml, typeof(TextAsset), false);
            _texture = (Texture2D)EditorGUILayout.ObjectField("Texture", _texture, typeof(Texture2D), false);

            if (GUILayout.Button(nameof(Slice)))
            {
                if (_xml == null || _texture == null)
                {
                    EditorUtility.DisplayDialog("ERROR", "XMLファイルと対象のテクスチャの両方を指定してください。", "OK");
                    return;
                }

                Slice();
            }
        }

        private void Slice()
        {
            var texturePath = AssetDatabase.GetAssetPath(_texture);
            var importer = AssetImporter.GetAtPath(texturePath) as TextureImporter;

            if (importer == null)
            {
                Debug.LogError("対象のテクスチャからTextureImporterを取得できませんでした。");
                return;
            }

            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.filterMode = FilterMode.Point;
            importer.textureCompression = TextureImporterCompression.Uncompressed;

            // XMLのパース & SpriteMetaDataを作成
            var metas = new List<SpriteMetaData>();
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(_xml.text);

                // <key>frames</key> の直後にある <dict> ノードを取得
                var framesDict = xmlDoc.SelectSingleNode("//dict/key[.='frames']/following-sibling::dict[1]");
                if (framesDict == null)
                {
                    Debug.LogError("XMLファイル内に 'frames' の定義が見つかりません。");
                    return;
                }

                // frames内の全てのキー(ファイル名)を取得
                var frameKeys = framesDict.SelectNodes("key");
                foreach (XmlNode keyNode in frameKeys)
                {
                    // キーの次にある<dict>ノード（フレーム情報）を取得
                    var frameDataDict = keyNode.NextSibling;

                    // フレームの矩形情報を取得
                    var frameRectNode = frameDataDict.SelectSingleNode("key[.='frame']/following-sibling::string[1]");
                    if (frameRectNode != null)
                    {
                        var rect = ParseRect(frameRectNode.InnerText);

                        // UnityのSprite Editorは左下原点、多くのツールは左上原点のためY座標を変換する
                        rect.y = _texture.height - rect.y - rect.height;

                        var meta = new SpriteMetaData
                        {
                            name = (Path.GetFileNameWithoutExtension(keyNode.InnerText)),
                            rect = rect,
                            alignment = (int)SpriteAlignment.Center,
                            pivot = new Vector2(0.5f, 0.5f),
                        };
                        metas.Add(meta);
                    }
                }
            }
            catch (System.Exception e)
            {
                EditorUtility.DisplayDialog("ERROR", "XMLファイルの解析中にエラーが発生しました。\n\n" + e.Message, "OK");
                Debug.LogError(e);
                return;
            }

            // SpriteMetaDataを適用して再インポート
            importer.spritesheet = metas.ToArray();
            importer.SaveAndReimport();

            Debug.Log($"SUCCESS: {_texture.name} に {metas.Count}個のスプライトを作成しました。");
            EditorUtility.DisplayDialog("SUCCESS", $"スライスが完了しました。\n{metas.Count}個のスプライトが作成されました。", "OK");
        }

        /// <summary>
        /// "{{x,y},{w,h}}" 形式の文字列をRectに変換する
        /// </summary>
        private static Rect ParseRect(string rectString)
        {
            // 例: "{{162,243},{80,80}}"
            var parts = rectString.Replace("{", "").Replace("}", "").Split(',');
            if (parts.Length == 4)
            {
                var x = float.Parse(parts[0]);
                var y = float.Parse(parts[1]);
                var width = float.Parse(parts[2]);
                var height = float.Parse(parts[3]);
                return new Rect(x, y, width, height);
            }

            Debug.LogWarning($"不正なRect文字列です: {rectString}");
            return Rect.zero;
        }
    }
}