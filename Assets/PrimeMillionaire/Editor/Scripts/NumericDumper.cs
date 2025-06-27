using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Text;
using UniEx;
using UnityEditor;

namespace PrimeMillionaire.Editor.Scripts
{
    public static class NumericDumper
    {
        [MenuItem("Tools/Numeric/" + nameof(DumpPrimeNumber))]
        private static void DumpPrimeNumber()
        {
            var list = GetNumbers()
                .Select(int.Parse)
                .Where(IsPrime);

            DumpCsv(list, "PrimeNumber");
        }

        [MenuItem("Tools/Numeric/" + nameof(DumpPalindromicNumber))]
        private static void DumpPalindromicNumber()
        {
            var list = GetNumbers()
                .Where(s => s.SequenceEqual(s.Reverse()))
                .Select(int.Parse);

            DumpCsv(list, "PalindromicNumber");
        }

        [MenuItem("Tools/Numeric/" + nameof(DumpHarshadNumber))]
        private static void DumpHarshadNumber()
        {
            var list = GetNumbers()
                .Select(int.Parse)
                .Where(IsHarshad);

            DumpCsv(list, "HarshadNumber");
        }

        [MenuItem("Tools/Numeric/" + nameof(DumpSquareNumber))]
        private static void DumpSquareNumber()
        {
            var list = GetNumbers()
                .Select(int.Parse)
                .Where(IsSquare);

            DumpCsv(list, "SquareNumber");
        }

        [MenuItem("Tools/Numeric/" + nameof(DumpTriangularNumber))]
        private static void DumpTriangularNumber()
        {
            var list = GetNumbers()
                .Select(int.Parse)
                .Where(IsTriangular);

            DumpCsv(list, "TriangularNumber");
        }

        [MenuItem("Tools/Numeric/" + nameof(DumpFibonacciNumber))]
        private static void DumpFibonacciNumber()
        {
            var list = GetNumbers()
                .Select(int.Parse)
                .Where(IsFibonacci);

            DumpCsv(list, "FibonacciNumber");
        }

        [MenuItem("Tools/Numeric/" + nameof(DumpMersenneNumber))]
        private static void DumpMersenneNumber()
        {
            var list = GetNumbers()
                .Select(int.Parse)
                .Where(IsMersenne);

            DumpCsv(list, "MersenneNumber");
        }


        private static IEnumerable<string> GetNumbers()
        {
            return Enumerable.Range(1, 13)
                .SelectMany(_ => Enumerable.Range(1, 13), (i, j) => (i, j))
                .SelectMany(_ => Enumerable.Range(1, 13), (t, k) => ZString.Concat(t.i, t.j, k))
                .Distinct();
        }

        private static bool IsPrime(int value)
        {
            if (value.IsEven()) return false;

            for (int i = 3; i * i <= value; i += 2)
            {
                if (value % i == 0) return false;
            }

            return true;
        }

        private static bool IsHarshad(int value)
        {
            int sum = 0, m = value;
            while (m > 0)
            {
                sum += m % 10;
                m /= 10;
            }

            return sum != 0 && value % sum == 0;
        }

        private static bool IsSquare(int value)
        {
            int root = (int)Mathf.Sqrt(value);
            return root * root == value;
        }

        private static bool IsSquare(long value)
        {
            int root = (int)Mathf.Sqrt(value);
            return root * root == value;
        }

        private static bool IsTriangular(int value)
        {
            return IsSquare(8 * value + 1);
        }

        private static bool IsFibonacci(int value)
        {
            long x = 5L * value * value;
            return IsSquare(x + 4) || IsSquare(x - 4);
        }

        private static bool IsMersenne(int value)
        {
            int p = 1;
            while (true)
            {
                long m = (1L << p) - 1; // 2^p - 1
                if (m == value) return true;
                if (m > value) return false;
                p++;
            }
        }

        private static void DumpCsv(IEnumerable<int> list, string fileName)
        {
            using var sb = ZString.CreateStringBuilder();
            foreach (var i in list.OrderBy(x => x))
            {
                sb.AppendLine(i);
            }

            File.WriteAllText(ZString.Format("../PrimeMillionaire/Master/Csv/{0}.csv", fileName), sb.ToString());
        }
    }
}