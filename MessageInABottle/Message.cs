using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MessageInABottle
{
    internal class Message
    {
        private static void Main(string[] args)
        {
            var encodedMessage = Console.ReadLine();
            var cypher = Console.ReadLine();

            var dict = ConvertCypher(cypher);

            var messages = new List<string>();

            DecodeMessages(encodedMessage, messages, dict);

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

        private static void DecodeMessages(string input, IList<string> output, IDictionary<string, char> dict, string preceding = "")
        {
            if (input == string.Empty)
            {
                output.Add(preceding);
                return;
            }

            for (int i = 0; i < input.Length; i++)
            {
                var splitIndex = i + 1;

                var currentSubstr = input.Substring(0, splitIndex);
                var nextSubstr = input.Substring(splitIndex);

                if (!dict.ContainsKey(currentSubstr))
                {
                    continue;
                }

                var letter = dict[currentSubstr];

                DecodeMessages(nextSubstr, output, dict, string.Concat(preceding, letter));
            }
        }
    }
}