using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static string[] results;
        static TextReader Console = System.Console.In;
        static void Main(string[] args)
        {
            var testFile = "..\\..\\..\\tests\\01";
            if (File.Exists(testFile))
                Console = new StreamReader(testFile);

            int count = int.Parse(Console.ReadLine());
            var lines = ReadStrLines(count);
            results = new string[count];

            for (int i = 0; i < count; i++)
            {
                results[i] = Calc(lines[i]);
            }

            WriteResults(results);
        }

        static string Calc(string password)
        {
            var isValidLower = IsValidLower(password);
            var isValidUpper = IsValidUpper(password);
            var isValidVowel = IsValidVowel(password);
            var isValidConsonant = IsValidConsonant(password);
            var isValidDigit = IsValidDigit(password);

            if (!isValidDigit)
                password += "1";

            if (!isValidLower & !isValidVowel & !isValidUpper &!isValidConsonant)
                return password + "aS";

            if (!isValidLower & !isValidVowel & !isValidUpper)
                password += "aS";

            if (!isValidLower & !isValidVowel & !isValidConsonant)
                password += "aS";

            if (!isValidVowel & !isValidUpper & !isValidConsonant)
                return password + "aS";

            if (!isValidLower & !isValidUpper & !isValidConsonant)
                return password + "aS";

            if (!isValidLower & !isValidUpper)
                return password + "aS";

            if (!isValidLower & !isValidVowel)
                return password + "a";

            if (!isValidUpper & !isValidVowel)
                return password + "A";

            if (!isValidLower & !isValidConsonant)
                return password + "s";

            if (!isValidUpper & !isValidConsonant)
                return password + "S";

            if (!isValidVowel)
                return password + "a";

            if (!isValidConsonant)
                return password + "S";

            if (!isValidUpper)
                return password + "D";

            if (!isValidLower)
                return password + "c";

            return password;
        }
        static bool IsValidUpper(string login)
        {
            for (int i = 0; i < login.Length; i++)
            {
                if (IsUpperLetter(login[i]))
                {
                    return true;
                }
            }

            return false;
        }
        static bool IsValidLower(string login)
        {
            for (int i = 0; i < login.Length; i++)
            {
                if (IsLowerLetter(login[i]))
                {
                    return true;
                }
            }

            return false;
        }

        static bool IsValidVowel(string login)
        {
            for (int i = 0; i < login.Length; i++)
            {
                if (IsVowelLetter(login[i]))
                {
                    return true;
                }
            }

            return false;
        }

        static bool IsValidConsonant(string login)
        {
            for (int i = 0; i < login.Length; i++)
            {
                if (IsConsonantLetter(login[i]))
                {
                    return true;
                }
            }

            return false;
        }
        static bool IsValidDigit(string login)
        {
            for (int i = 0; i < login.Length; i++)
            {
                if (IsDigit(login[i]))
                {
                    return true;
                }
            }

            return false;
        }

        static bool IsVowelLetter(char ch)
        {
            if (ch == 'e')
                return true;
            if (ch == 'u')
                return true;
            if (ch == 'i')
                return true;
            if (ch == 'o')
                return true;
            if (ch == 'a')
                return true;
            if (ch == 'y')
                return true;


            if (ch == 'E')
                return true;
            if (ch == 'U')
                return true;
            if (ch == 'I')
                return true;
            if (ch == 'O')
                return true;
            if (ch == 'A')
                return true;
            if (ch == 'Y')
                return true;


            return false;
        }
        static bool IsConsonantLetter(char ch)
        {
            if (ch <= 90 & ch >= 65)
                return !IsVowelLetter(ch);

            if (ch <= 122 & ch >= 97)
                return !IsVowelLetter(ch);

            return false;
        }

        static bool IsUpperLetter(char ch)
        {
            if (ch <= 90 & ch >= 65)
                return true;

            return false;
        }
        static bool IsLowerLetter(char ch)
        {
            if (ch <= 122 & ch >= 97)
                return true;

            return false;
        }
        static bool IsDigit(char ch)
        {
            return ch <= 57 & ch >= 48;
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
