using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static bool[] results;
        static TextReader Console = System.Console.In;
        static void Main(string[] args)
        {
            var testFile = "..\\..\\..\\tests\\01";
            if (File.Exists(testFile))
                Console = new StreamReader(testFile);

            int dataSetsCount = int.Parse(Console.ReadLine());
            results = new bool[dataSetsCount];
            var lines = new string[dataSetsCount][];


            for (int i = 0; i < dataSetsCount; i++)
            {
                int count = int.Parse(Console.ReadLine());
                lines[i] = ReadStrLines(count);
            }

            Calc(lines);

            WriteResults(results);
        }
        static async void Calc(string[][] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                results[i] = await Calc(lines[i]);
            }
        }
        static async Task<bool> Calc(string[] lines)
        {
            var dates = new DateTime[lines.Length][];

            for (int i = lines.Length-1; i >= 0; i--)
            {
                var line = lines[i].Split("-");
                DateTime d1, d2;
                if (!TryParseDate(line[0], out d1))
                    return false;
                if (!TryParseDate(line[1], out d2))
                    return false;
                if (d1 > d2)
                    return false;

                dates[i] = new DateTime[] { d1, d2 };
            }

            var orderedDates = dates.OrderBy(d => d[0]).ToArray();
            for (int i = 0; i < orderedDates.Length - 1; i++)
                if (orderedDates[i][1]>= orderedDates[i+1][0])
                    return false;

            return true;
        }
        static bool TryParseDate(string line, out DateTime date)
        {
            var items = line.Split(":");
            var h = int.Parse(items[0]);
            var m = int.Parse(items[1]);
            var s = int.Parse(items[2]);

            if (h >= 0 & h <= 23 & m >= 0 & m <= 59 & s >= 0 & s <= 59)
            {
                date = new DateTime(2022, 1, 1, h, m, s);
                return true;
            }
            date = DateTime.MinValue;
            return false;
        }
        static void WriteResults(bool[] dataSets)
        {
            var sb = new StringBuilder();
            foreach (var dataSetCorrect in dataSets)
                if (dataSetCorrect)
                    sb.AppendLine("YES");
                else
                    sb.AppendLine("NO");
            System.Console.WriteLine(sb.ToString());
        }       
        static string[] ReadStrLines(int n)
        {
            string[] dataSet = new string[n];
            for (int i = 0; i < n; i++)
                dataSet[i] = Console.ReadLine();

            return dataSet;
        }
    }
}
