using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static int[][][] results;
        static TextReader Console = System.Console.In;
        static void Main(string[] args)
        {
            var testFile = "..\\..\\..\\tests\\01";
            if (File.Exists(testFile))
                Console = new StreamReader(testFile);

            int dataSetsCount = int.Parse(Console.ReadLine());
            results = new int[dataSetsCount][][];

            for (int i = 0; i < dataSetsCount; i++)
            {
                int someCount1 = int.Parse(Console.ReadLine());
                var initField = ReadIntLine();
                results[i] = Calc2(initField);
            }

            WriteResults(results);
        }

        static int[][] Calc2(int[] input)
        {
            var dict = new int[input.Length][];
            var selected = new bool[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                int quality = input[i];

                var d = new SortedList<int, List<int>>();
                for (int j = 0; j < input.Length; j++)
                {
                    if (i != j)
                    {
                        int rasnitsa = Math.Abs(quality- input[j]);
                        if (!d.ContainsKey(rasnitsa))
                        {
                            var l = new List<int>(10);
                            l.Add(j);
                            d.Add(rasnitsa, l);
                        }
                        else
                            d[rasnitsa].Add(j);
                    }
                }

                var sorted = new int[input.Length-1];
                int h = 0;
                foreach (var item in d)
                {
                    foreach (var subItem in item.Value)
                    {
                        sorted[h] = subItem;
                        h++;
                    }
                }
                dict[i] = sorted;
            }

            int count = input.Length / 2;
            int[][] result = new int[count][];

            int c = 0;
            for (int i = 0; i < dict.Length; i++)
            {
                if (!selected[i])
                {
                    selected[i] = true;
                    int second = 0;
                    foreach (var item in dict[i])
                    {
                        if (!selected[item])
                        {
                            second = item;
                            selected[item] = true;
                            break;
                        }
                    }

                    result[c] = new int[] { i, second };
                    c++;

                }
            }

            return result;
        }


        static int[][] Calc(int[] input)
        {
            int count = input.Length / 2;

            var dict = new SortedList<int, List<int>>();
            for (int i = 0; i < input.Length; i++)
            {
                if (!dict.ContainsKey(input[i]))
                {
                    var l = new List<int>(10);
                    l.Add(i);
                    dict.Add(input[i], l);
                }
                else
                    dict[input[i]].Add(i);
            }

            var sorted = new int[input.Length];
            int j = 0;
            foreach (var item in dict)
            {
                foreach (var subItem in item.Value)
                {
                    sorted[j] = subItem;
                    j++;
                }
            }

            int[][] result = new int[count][];

            for (int i = 0; i < count; i++)
            {
                int c = i * 2;
                result[i] = new int[] { sorted[c], sorted[c + 1] };
            }

            return result;
        }

        static void WriteResults(int[][][] dataSets)
        {
            var sb = new StringBuilder();
            foreach (var table in dataSets)
            {
                foreach (var row in table)
                {
                    foreach (var cell in row)
                        sb.Append((cell+1).ToString() + " ");
                    sb.AppendLine();
                }
                sb.AppendLine();
            }
            System.Console.WriteLine(sb.ToString());
        }

        static int[] ReadIntLine()
        {
            string line = Console.ReadLine();
            int[] lineItems = line.Split(' ').Select(it => int.Parse(it)).ToArray();
            return lineItems;
        }        
    }
}
