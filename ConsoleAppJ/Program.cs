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
        static string[] results;
        static string[] words;
        static string first;
        static Dictionary<string, List<string>> dict;
        static TextReader Console = System.Console.In;
        static void Main(string[] args)
        {
            var testFile = "..\\..\\..\\tests\\01";
            if (File.Exists(testFile))
                Console = new StreamReader(testFile);

            int dictCount = int.Parse(Console.ReadLine());
            var dictWords = ReadStrLines(dictCount);
            first = dictWords[0];
            dict = new Dictionary<string, List<string>>(50000 * 10);
            foreach (var word in dictWords)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    var subStr = word.Substring(i, word.Length-i);

                    if (dict.ContainsKey(subStr))
                        dict[subStr].Add(word);
                    else
                    {
                        var l = new List<string>();
                        l.Add(word);
                        dict.Add(subStr, l);
                    }
                }
            }

            int wordsCount = int.Parse(Console.ReadLine());
            words = ReadStrLines(wordsCount);
            results = new string[wordsCount];

            Calc();

            WriteResults(results);
        }

        static void Calc()
        {
            for (int i = 0; i < words.Length; i++)
                results[i] = Calc(words[i]);
        }
        static string Calc(string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                var subStr = word.Substring(i, word.Length - i);
                if (dict.ContainsKey(subStr))
                {
                    var res = dict[subStr];
                    foreach (var dictWord in res)
                        if (dictWord != word)
                            return dictWord;
                }
            }
            return first;            
        }

        static void WriteResults(string[] dataSet)
        {
            var sb = new StringBuilder();
            foreach (var item in dataSet)
                sb.AppendLine(item);
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
