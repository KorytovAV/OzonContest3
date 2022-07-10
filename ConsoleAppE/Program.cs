using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

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

            for (int i = 0; i < dataSetsCount; i++)
            {
                int count = int.Parse(Console.ReadLine());

                var tasks = ReadIntLine();
                results[i] = Calc(tasks);
            }

            WriteResults(results);
        }

        static bool Calc(int[] tasks)
        {
            var finishedTasks = new HashSet<int>(tasks.Length);

            for (int i = 0; i < tasks.Length-1; i++)
            {
                var task = tasks[i];
                var nextTask = tasks[i+1];

                if (finishedTasks.Contains(task))
                    return false;
                if (task != nextTask)
                    finishedTasks.Add(task);
            }

            var lastTask = tasks[tasks.Length - 1];
            if (finishedTasks.Contains(lastTask))
                return false;

            return true;
        }

        static void WriteResultsInLine(int[] dataSet)
        {
            var sb = new StringBuilder();
            foreach (var item in dataSet)
                sb.Append(item.ToString() + " ");
            System.Console.WriteLine(sb.ToString());
        }
        static void WriteResults(int[] dataSet)
        {
            var sb = new StringBuilder();
            foreach (var item in dataSet)
                sb.AppendLine(item.ToString());
            System.Console.WriteLine(sb.ToString());
        }
        static void WriteResults(int[][] dataSets)
        {
            var sb = new StringBuilder();
            foreach (var dataSet in dataSets)
            {
                foreach (var item in dataSet)
                    sb.Append(item.ToString() + " ");
                sb.AppendLine();
            }
            System.Console.WriteLine(sb.ToString());
        }
        static void WriteResults(int[][][] dataSets)
        {
            var sb = new StringBuilder();
            foreach (var table in dataSets)
            {
                foreach (var row in table)
                {
                    foreach (var cell in row)
                        sb.Append(cell.ToString() + " ");
                    sb.AppendLine();
                }
                sb.AppendLine();
            }
            System.Console.WriteLine(sb.ToString());
        }
        static void WriteResults(char[][][] dataSets)
        {
            var sb = new StringBuilder();
            foreach (var dataSet in dataSets)
            {
                foreach (var line in dataSet)
                    sb.AppendLine(new string(line));
                sb.AppendLine();
            }
            System.Console.WriteLine(sb.ToString());
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
        static void WriteResults(Dictionary<string, List<string>>[] results)
        {
            var sb = new StringBuilder();
            foreach (var dataSet in results)
            {
                var names = dataSet.Keys.OrderBy(k => k);
                foreach (var name in names)
                {
                    sb.Append(name + ": " + dataSet[name].Count + " ");

                    for (int i = 0; i < dataSet[name].Count; i++)
                        sb.Append(dataSet[name][i].ToString() + " ");
                    sb.AppendLine();
                }
                sb.AppendLine();
            }
            sb.AppendLine();
            System.Console.WriteLine(sb.ToString());
        }
        static void WriteResults(List<string>[][] dataSets)
        {
            var sb = new StringBuilder();
            foreach (var dataSet in dataSets)
            {
                foreach (var modul in dataSet)
                {
                    sb.Append(modul.Count + " ");
                    foreach (var subModul in modul)
                        sb.Append(subModul + " ");
                    sb.AppendLine();
                }
                sb.AppendLine();
            }
            System.Console.WriteLine(sb.ToString());
        }
        static void WriteResults(bool[][] queriesStatus, int[][][] dataSet)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < queriesStatus.Length; i++)
            {
                for (int j = 0; j < queriesStatus[i].Length; j++)
                {
                    sb.Append(queriesStatus[i][j] ? "SUCCESS" : "FAIL");
                    if (queriesStatus[i][j] & dataSet[i][j] != null)
                        sb.Append(" " + dataSet[i][j][0].ToString() + "-" + dataSet[i][j][1].ToString());
                    sb.AppendLine();
                }
                sb.AppendLine();
            }
            System.Console.WriteLine(sb.ToString());
        }


        static char[][] ReadThreeCharLine()
        {
            char[][] dataSet = new char[3][];
            dataSet[0] = Console.ReadLine().ToCharArray();
            dataSet[1] = Console.ReadLine().ToCharArray();
            dataSet[2] = Console.ReadLine().ToCharArray();

            return dataSet;
        }
        static int[] ReadIntLine()
        {
            string line = Console.ReadLine();
            int[] lineItems = line.Split(' ').Select(it => int.Parse(it)).ToArray();
            return lineItems;
        }
        static int[][] ReadIntLines(int n)
        {
            int[][] dataSet = new int[n][];
            for (int i = 0; i < n; i++)
                dataSet[i] = Console.ReadLine().Split(' ').Select(it => int.Parse(it)).ToArray();

            return dataSet;
        }
        static string[] ReadStrLines(int n)
        {
            string[] dataSet = new string[n];
            for (int i = 0; i < n; i++)
                dataSet[i] = Console.ReadLine();

            return dataSet;
        }
        static char[][] ReadCharLines(int n)
        {
            char[][] dataSet = new char[n][];
            for (int i = 0; i < n; i++)
                dataSet[i] = Console.ReadLine().ToCharArray();

            return dataSet;
        }

        static Dictionary<string, List<string>> ParseDepend(string[] lines)
        {
            var result = new Dictionary<string, List<string>>();
            foreach (var line in lines)
            {
                var names = line.Split(":");
                var modul = names[0];
                var subModulsNames = names[1].Trim().Split(" ");

                var subModuls = new List<string>(10);
                foreach (var subModulsName in subModulsNames)
                    if (subModulsName != "")
                        subModuls.Add(subModulsName);
                result.Add(modul, subModuls);
            }
            return result;
        }
        static bool[] IsValid(string[] input)
        {
            bool[] result = new bool[input.Length];
            HashSet<string> dict = new HashSet<string>(input.Length);

            for (int i = 0; i < input.Length; i++)
            {
                var login = input[i].ToLower();

                bool isValid = IsValid(login);

                if (isValid)
                {
                    if (dict.Contains(login))
                        isValid = false;
                    else dict.Add(login);
                }
                result[i] = isValid;
            }

            return result;
        }
        static bool IsValid(string login)
        {
            if (login.Length < 2 || login.Length > 24)
                return false;

            if (login[0] == '-')
                return false;

            foreach (var ch in login)
                if (!IsValid(ch))
                    return false;

            return true;
        }
        static bool IsValid(char ch)
        {
            return IsDigit(ch) || IsLetter(ch) || IsSymbol(ch);
        }
        static bool IsSymbol(char ch)
        {
            return ch == 45 || ch == 95;
        }
        static bool IsLetter(char ch)
        {
            if (ch <= 90 & ch >= 65)
                return true;

            if (ch <= 122 & ch >= 97)
                return true;

            return false;
        }
        static bool IsDigit(char ch)
        {
            return ch <= 57 & ch >= 48;
        }
    }
}
