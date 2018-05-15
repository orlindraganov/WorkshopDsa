using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerRanking
{
    class Ranking
    {
        private const string end = "end";

        static void Main(string[] args)
        {
            var ranklist = new List<Player>();
            var playersByType = new Dictionary<string, IList<Player>>();

            var command = Command.Parse(Console.ReadLine());

            while (command.Name != end)
            {
                command.Execute(ranklist, playersByType);
                command = Command.Parse(Console.ReadLine());
            }
        }
    }

    internal class Player : IComparable<Player>
    {
        public Player(string name, string type, int age)
        {
            this.Name = name;
            this.Type = type;
            this.Age = age;
        }

        public string Name { get; private set; }
        public string Type { get; private set; }
        public int Age { get; private set; }

        public int CompareTo(Player other)
        {
            var output = this.Name.CompareTo(other.Name);
            if (output == 0)
            {
                output = other.Age.CompareTo(this.Age);
            }

            return output;
        }

        public override string ToString()
        {
            return $"{this.Name}({this.Age})";
        }
    }

    internal class Command
    {
        public static Command Parse(string s)
        {
            var com = s.Split().ToList();
            var name = com[0];
            com.RemoveAt(0);
            var args = com.ToArray();

            return new Command(name, args);
        }

        public Command(string name, string[] args)
        {
            this.Name = name;
            this.Args = args;
        }

        public string Name { get; set; }
        public string[] Args { get; set; }

        public void Execute(IList<Player> ranklist, IDictionary<string, IList<Player>> playersByType)
        {
            switch (this.Name.ToLower())
            {
                case "add":
                    var name = this.Args[0];
                    var type = this.Args[1];
                    var age = int.Parse(this.Args[2]);
                    var position = int.Parse(this.Args[3]) - 1;

                    var player = new Player(name, type, age);

                    ranklist.Insert(position, player);

                    if (!playersByType.ContainsKey(type))
                    {
                        playersByType.Add(type, new List<Player>());
                    }

                    playersByType[type].Add(player);

                    Console.WriteLine($"Added player {name} to position {position + 1}");
                    break;

                case "find":
                    type = this.Args[0];
                    var max = 5;
                    var players = new List<Player>(max);

                    playersByType[type] = playersByType[type].OrderBy(p => p).ToList();

                    for (int i = 0; i < max && i < playersByType[type].Count; i++)
                    {
                        players.Add(playersByType[type][i]);
                    }

                    var playersStr = string.Join("; ", players);
                    Console.WriteLine($"Type {type}: {playersStr}");
                    break;

                case "ranklist":
                    var start = int.Parse(Args[0]);
                    var end = int.Parse(Args[1]);

                    var results = new List<string>();
                    for (int i = start; i < end + 1; i++)
                    {
                        var index = i - 1;

                        results.Add($"{i}. {ranklist[index]}");
                    }

                    var resultStr = string.Join("; ", results);

                    Console.WriteLine(resultStr);
                    break;

                default:
                    throw new ArgumentException("Error dage");
            }
        }
    }
}
