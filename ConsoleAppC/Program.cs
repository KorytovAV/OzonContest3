using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static List<int> results;
        static TextReader Console = System.Console.In;
        static void Main(string[] args)
        {
            var testFile = "..\\..\\..\\tests\\01";
            if (File.Exists(testFile))
                Console = new StreamReader(testFile);

            var initField = ReadIntLine();

            int usersCount = initField[0];
            int requestsCount = initField[1];
            int messagesCount = 0;

            results = new List<int>(requestsCount);

            var messages = new int[usersCount];

            for (int i = 0; i < requestsCount; i++)
            {
                initField = ReadIntLine();
                var requestType = initField[0];
                var user = initField[1];

                if (requestType == 1)
                {
                    messagesCount++;
                    if (user == 0)
                    {
                        //for (int j = 0; j < messages.Length; j++)
                        //    messages[j] = messagesCount;
                        Array.Fill(messages, messagesCount);
                    }
                    else
                    {
                        messages[user - 1] = messagesCount;
                    }
                }
                else if (requestType == 2)
                {
                    results.Add(messages[user - 1]);
                }
            }

            WriteResults(results);
        }

        static void WriteResults(List<int> dataSet)
        {
            var sb = new StringBuilder();
            foreach (var item in dataSet)
                sb.AppendLine(item.ToString());
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
