using System.Diagnostics;
using System.IO;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace PrimeMillionaire.Editor.Scripts
{
    public sealed class AudioConverter
    {
        private const string MENU_PATH = "Tools/AudioConverter/";

        private const string SOURCE_DIR = "Sound/Source/";
        private const string OUTPUT_DIR = "Sound/Output/";
        private const string FFMPEG = "/usr/local/bin/ffmpeg";

        [MenuItem(MENU_PATH + nameof(ConvertM4AToMp3))]
        private static void ConvertM4AToMp3()
        {
            Debug.Log($"[START] {nameof(ConvertM4AToMp3)}");

            Convert("mp3", "-vn -acodec libmp3lame -b:a 192k -y");

            Debug.Log($"[FINISH] {nameof(ConvertM4AToMp3)}");
        }

        [MenuItem(MENU_PATH + nameof(ConvertM4AToWav))]
        private static void ConvertM4AToWav()
        {
            Debug.Log($"[START] {nameof(ConvertM4AToWav)}");

            Convert("wav", "-vn -acodec pcm_s16le -ar 44100 -y");

            Debug.Log($"[FINISH] {nameof(ConvertM4AToWav)}");
        }

        private static void Convert(string ext, string opt)
        {
            var m4as = Directory.GetFiles(SOURCE_DIR, "*.m4a");
            if (m4as.Length == 0)
            {
                Debug.Log($"Target file is nothing.");
                return;
            }

            foreach (var m4a in m4as)
            {
                var output = Path.Combine(OUTPUT_DIR, $"{Path.GetFileNameWithoutExtension(m4a)}.{ext}");

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = FFMPEG,
                    Arguments = $"-i \"{m4a}\" {opt} \"{output}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                };

                var process = Process.Start(processStartInfo);
                if (process is null)
                {
                    Debug.LogError($"Failed to start process: {m4a}");
                    continue;
                }

                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    Debug.Log($"Succeeded to convert: {m4a} -> {output}");
                }
                else
                {
                    Debug.LogError($"Failed to convert: {m4a}");
                }
            }
        }
    }
}