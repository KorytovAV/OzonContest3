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
                var rows = ReadIntLine()[0];
                var map = new Map(ReadCharLines(rows));

                results[i] = map.CheckMapValid();
            }

            WriteResults(results);
        }

        private class Map
        {
            public Map(char[][] colors)
            {
                Colors = colors;
            }

            public char[][] Colors { get; }

            public bool CheckMapValid()
            {
                var colorsCheck = new bool[Colors.Length][];
                for (int i = 0; i < Colors.Length; i++)
                    colorsCheck[i] = new bool[Colors[i].Length];

                var prevColors = new HashSet<char>(30);
                for (int i = 0; i < Colors.Length; i++)
                {
                    for (int j = i % 2; j < Colors[i].Length; j += 2)
                    {
                        if (!colorsCheck[i][j])
                        {
                            if (prevColors.Contains(Colors[i][j]))
                                return false;

                            prevColors.Add(Colors[i][j]);
                            CheckMapColor(colorsCheck, i, j);
                        }
                    }
                }
                return true;
            }

            private void CheckMapColor(bool[][] colorsCheck, int row, int column)
            {
                colorsCheck[row][column] = true;

                int neighborRow;
                int neighborCol;

                //верхняя строка
                if (row > 0)
                {
                    neighborRow = row - 1;
                    if (column>0)
                    {
                        neighborCol= column-1;
                        if (!colorsCheck[neighborRow][neighborCol] && Colors[row][column]== Colors[neighborRow][neighborCol])
                            CheckMapColor(colorsCheck, neighborRow, neighborCol);
                    }

                    if (column < colorsCheck[neighborRow].Length - 1)
                    {
                        neighborCol = column + 1;
                        if (!colorsCheck[neighborRow][neighborCol] && Colors[row][column] == Colors[neighborRow][neighborCol])
                            CheckMapColor(colorsCheck, neighborRow, neighborCol);
                    }
                }

                //текущая строка
                neighborRow = row;
                if (column > 1)
                {
                    neighborCol = column - 2;
                    if (!colorsCheck[neighborRow][neighborCol] && Colors[row][column] == Colors[neighborRow][neighborCol])
                        CheckMapColor(colorsCheck, neighborRow, neighborCol);
                }

                if (column < colorsCheck[neighborRow].Length - 2)
                {
                    neighborCol = column + 2;
                    if (!colorsCheck[neighborRow][neighborCol] && Colors[row][column] == Colors[neighborRow][neighborCol])
                        CheckMapColor(colorsCheck, neighborRow, neighborCol);
                }

                //нижняя строка
                if (row < colorsCheck.Length-1)
                {
                    neighborRow = row + 1;
                    if (column > 0)
                    {
                        neighborCol = column - 1;
                        if (!colorsCheck[neighborRow][neighborCol] && Colors[row][column] == Colors[neighborRow][neighborCol])
                            CheckMapColor(colorsCheck, neighborRow, neighborCol);
                    }

                    if (column < colorsCheck[neighborRow].Length - 1)
                    {
                        neighborCol = column + 1;
                        if (!colorsCheck[neighborRow][neighborCol] && Colors[row][column] == Colors[neighborRow][neighborCol])
                            CheckMapColor(colorsCheck, neighborRow, neighborCol);
                    }
                }
            }
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
        static char[][] ReadCharLines(int n)
        {
            char[][] dataSet = new char[n][];
            for (int i = 0; i < n; i++)
                dataSet[i] = Console.ReadLine().ToCharArray();

            return dataSet;
        }
    }
}
