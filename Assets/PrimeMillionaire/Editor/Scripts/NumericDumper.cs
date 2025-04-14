using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Text;
using UnityEditor;

namespace PrimeMillionaire.Editor.Scripts
{
    public static class NumericDumper
    {
        [MenuItem("Tools/Numeric/" + nameof(DumpPrimeNumber))]
        private static void DumpPrimeNumber()
        {
            var list = new List<int>();
            for (int i = 1; i <= 13; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    // NOTE: 偶数は見ない
                    for (int k = 1; k <= 13; k += 2)
                    {
                        var value = int.Parse(ZString.Concat(i, j, k));
                        if (!IsPrime(value)) continue;
                        if (list.Any(x => x == value)) continue;

                        list.Add(value);
                    }
                }
            }

            using var sb = ZString.CreateStringBuilder();
            foreach (int i in list.OrderBy(x => x))
            {
                sb.AppendLine(i);
            }

            File.WriteAllText("Assets/Externals/Csv/PrimeNumber.csv", sb.ToString());
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