using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Slogan
{
    internal class Slogan
    {
        private const string NotValid = "NOT VALID";

        private static void Main()
        {
            var numberOfMessages = int.Parse(Console.ReadLine());

            var output = new List<string>();

            for (int i = 0; i < numberOfMessages; i++)
            {
                var wordsStr = Console.ReadLine();
                var message = Console.ReadLine();

                var separator = new[] { ' ' };
                var words = wordsStr.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            var sb = new StringBuilder();
            foreach (var msg in output)
            {
                sb.AppendLine(msg);
            }

            Console.WriteLine(sb.ToString());
        }

        private static bool DecodeMessage(string message, IEnumerable<string> words, ICollection<string> output)
        {
            var preceding = new List<string>();
            return DecodeMessage(message, words, output, preceding);
        }

        private static bool DecodeMessage(string message, IEnumerable<string> words, ICollection<string> output, ICollection<string> preceding)
        {
            if (message == string.Empty)
            {
                return true;
            }

            var isMessageValid = false;

            foreach (var word in words)
            {
                if (!message.StartsWith(word))
                {
                    continue;
                }

                isMessageValid = true;
                preceding.Add(word);

                var next = message.Substring(word.Length);

                if (DecodeMessage(next, words, output, preceding))
                {
                    output.Add(word);
                    return true;
                }
            }

            if (!isMessageValid)
            {
                output.Add(NotValid);
            }

            return false;
        }

    }
}
