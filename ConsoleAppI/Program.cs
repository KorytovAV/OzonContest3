using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static TextReader Console = System.Console.In;
        static void Main(string[] args)
        {
            var testFile = "..\\..\\..\\tests\\01";
            if (File.Exists(testFile))
                Console = new StreamReader(testFile);

            var intLine = ReadIntLine();
            int cpuCount = intLine[0];
            int taskCount = intLine[1];

            long energy = 0;

            var freeCPU = new SortedSet<long>(ReadLongLine());
            var stampsToRemove = new List<long>(Math.Min(cpuCount, taskCount));
            var currentTimeStep = 0;

            var tasks = ReadLongLines(taskCount,2);

            var taskEnds = tasks.Select(t => t[0] + t[1]).Distinct().OrderBy(t => t);

            var timeSteps = new List<KeyValuePair<long, SortedSet<long>>>(taskCount);
            var timeDictionary = new Dictionary<long, SortedSet<long>>(taskCount);
            foreach (var taskEnd in taskEnds)
            {
                var cpus = new SortedSet<long>();
                timeSteps.Add(new KeyValuePair<long, SortedSet<long>>(taskEnd, cpus));
                timeDictionary.Add(taskEnd, cpus);
            }

            for (int i = 0; i < taskCount; i++)
            {
                long taskBegin = tasks[i][0];
                long taskDuration = tasks[i][1];

                stampsToRemove.Clear();

                while (currentTimeStep < timeSteps.Count && taskBegin>=timeSteps[currentTimeStep].Key)
                {
                    if (timeSteps[currentTimeStep].Value.Count>0)
                        freeCPU.UnionWith(timeSteps[currentTimeStep].Value);
                    currentTimeStep++;
                }

                if (freeCPU.Count > 0)
                {
                    var cpu = freeCPU.Min;
                    energy += taskDuration * cpu;
                    var taskEnd = taskBegin + taskDuration;

                    timeDictionary[taskEnd].Add(cpu);
                    freeCPU.Remove(cpu);
                }
                else
                    continue;

            }

            System.Console.WriteLine(energy);
        }

        static int[] ReadIntLine()
        {
            var line = Console.ReadLine();
            var items = line.Split(' ').Select(it => int.Parse(it)).ToArray();
            return items;
        }

        static long[] ReadLongLine()
        {
            var line = Console.ReadLine();
            var items = line.Split(' ').Select(it => long.Parse(it)).ToArray();
            return items;
        }

        static long[][] ReadLongLines(int rows, int columns)
        {
            var sb = new StringBuilder(rows * 7 * columns - 1);
            sb.Append(Console.ReadLine());
            for (int i = 1; i < rows; i++)
            {
                sb.Append(' ');
                sb.Append(Console.ReadLine());
            }
            var allValues = sb.ToString().Split(' ').Select(it => long.Parse(it)).ToArray();

            var dataSet = new long[rows][];
            var k = 0;
            for (int i = 0; i < rows; i++)
            {
                var line = new long[columns];
                for (int j = 0; j < columns; j++, k++)
                    line[j] = allValues[k];
                dataSet[i] = line;
            }

            return dataSet;
        }
    }
}