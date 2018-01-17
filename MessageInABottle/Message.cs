using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MessageInABottle
{
    internal class Message
    {
        private static Dictionary<string, char> dict;

        private static void Main(string[] args)
        {
            var message = Console.ReadLine();
            var cypher = Console.ReadLine();

            dict = ConvertCypher(cypher);

            var messages = new List<string>();

            DecodeMessages(message, messages);

            Console.WriteLine(messages.Count);

            foreach (var msg in messages.OrderBy(x => x))
            {
                Console.WriteLine(msg);
            }
        }

        private static Dictionary<string, char> ConvertCypher(string cypher)
        {
            var pattern = @"[A-Z]\d+";
            var dict = new Dictionary<string, char>();

            var match = Regex.Match(cypher, pattern);
            while (match.Success)
            {
                var letter = match.Value[0];
                var code = match.Value.Substring(1);
                dict.Add(code, letter);

                match = match.NextMatch();
            }

            return dict;
        }

        private static void DecodeMessages(string input, IList<string> output, string preceding = "")
        {
            if (input == string.Empty)
            {
                return;
            }

            for (int i = 1; i <= input.Length; i++)
            {
                var currentSubstr = input.Substring(0, i);
                var nextSubstr = input.Substring(i);

                if (!dict.ContainsKey(currentSubstr))
                {
                    continue;
                }

                var letter = dict[currentSubstr];
                if (nextSubstr != string.Empty)
                {
                    DecodeMessages(nextSubstr, output, string.Concat(preceding, letter));
                }
                else
                {
                    output.Add(string.Concat(preceding, letter));
                }
            }
        }
    }
}