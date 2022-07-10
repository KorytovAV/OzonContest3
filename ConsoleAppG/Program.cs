using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static Dictionary<int, User> users = new Dictionary<int, User>();
        static TextReader Console = System.Console.In;
        static void Main(string[] args)
        {
            var testFile = "..\\..\\..\\tests\\01";
            if (File.Exists(testFile))
                Console = new StreamReader(testFile);


            var intLine = ReadIntLine();
            int usersCount = intLine[0];
            int pairsCount = intLine[1];

            if (pairsCount > 0)
            {
                var pairs = ReadIntLines(pairsCount, 2);

                for (int i = 0; i < pairsCount; i++)
                    AddFriends(pairs[i][0], pairs[i][1]);

                foreach (var user in users.Values)
                    user.RecalcPossibleFriends();
            }

            WriteResults(usersCount);
        }

        private class User : IComparable
        {
            public User(int id)
            {
                ID = id;
                Friends = new Dictionary<int, User>();
                PossibleFriends = null;
            }
            public override int GetHashCode()
            {
                return ID;
            }
            public override string ToString()
            {
                return ID.ToString();
            }
            public int ID { get; }
            public Dictionary<int, User> Friends { get; }
            public User[] PossibleFriends { get; private set;  }

            public void RecalcPossibleFriends()
            {
                var allPossibleFriends = new List<User>(Friends.Count * Friends.Count);
                foreach (var friend in Friends.Values)
                    allPossibleFriends.AddRange(friend.Friends.Values);

                var dict = new SortedDictionary<User, int>();
                foreach (var friend in allPossibleFriends)
                {
                    if (friend == this || Friends.ContainsKey(friend.ID))
                        continue;
                    if (dict.ContainsKey(friend))
                        dict[friend]++;
                    else
                        dict.Add(friend, 1);
                }


                if (dict.Values.Count > 0)
                {
                    var maxUsers = dict.Values.Max();
                    var dict2 = new List<User>();
                    foreach (var user in dict)
                    {
                        if (user.Value == maxUsers)
                            dict2.Add(user.Key);
                    }

                    PossibleFriends = dict2.ToArray();
                }
                else
                    PossibleFriends = null;
            }

            public int CompareTo(object obj)
            {
                return ID.CompareTo(obj.GetHashCode());
            }
        }

        private static void AddFriends(int userID1, int userID2)
        {
            User user1, user2;
            if (users.ContainsKey(userID1))
                user1 = users[userID1];
            else
            {
                user1 = new User(userID1);
                users.Add(userID1, user1);
            }

            if (users.ContainsKey(userID2))
                user2 = users[userID2];
            else
            {
                user2 = new User(userID2);
                users.Add(userID2, user2);
            }

            if (!user1.Friends.ContainsKey(userID2))
                user1.Friends.Add(userID2, user2);

            if (!user2.Friends.ContainsKey(userID1))
                user2.Friends.Add(userID1, user1);


        }

        static void WriteResults(int usersCount)
        {
            var sb = new StringBuilder();
            for (int userId = 1; userId <= usersCount; userId++)
                if (users.ContainsKey(userId))
                {
                    var user = users[userId];
                    if (user.PossibleFriends != null && user.PossibleFriends.Length > 0)
                    {
                        foreach (var friend in user.PossibleFriends)
                            sb.Append(friend.ToString() + " ");
                        sb.AppendLine();
                    }
                    else
                        sb.AppendLine("0");
                }
                else
                    sb.AppendLine("0");
            System.Console.WriteLine(sb.ToString());
        }
        static int[] ReadIntLine()
        {
            string line = Console.ReadLine();
            int[] lineItems = line.Split(' ').Select(it => int.Parse(it)).ToArray();
            return lineItems;
        }
        static int[][] ReadIntLines(int rows, int columns)
        {
            var sb = new StringBuilder(rows * 6 * columns-1);
            sb.Append(Console.ReadLine());
            for (int i = 1; i < rows; i++)
            {
                sb.Append(' ');
                sb.Append(Console.ReadLine());
            }
            var allValues = sb.ToString().Split(' ').Select(it => int.Parse(it)).ToArray();

            var dataSet = new int[rows][];
            var k = 0;
            for (int i = 0; i < rows; i++)
            {
                var line = new int[columns];
                for (int j = 0; j < columns; j++, k++)
                    line[j] = allValues[k];
                dataSet[i] = line;
            }

            return dataSet;
        }
    }
}
