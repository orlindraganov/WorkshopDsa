using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

namespace Slogan
{
    internal class Slogan
    {
        private const string NotValid = "NOT VALID";

        private static void Main()
        {
            var numberOfMessages = int.Parse(Console.ReadLine());
            var allMessages = new BigList<string>();

            for (int i = 0; i < numberOfMessages; i++)
            {
                var words = Console.ReadLine();
                var message = Console.ReadLine();
                var currMessages = new BigList<string>();
                
                CheckSlogan(message, words, currMessages);

                if (currMessages.Count > 0)
                {
                    allMessages.AddRange(currMessages.OrderBy(x => x));
                }
                else
                {
                    allMessages.Add(NotValid);
                }
            }

            var sb = new StringBuilder();
            foreach (var message in allMessages)
            {
                sb.AppendLine(message);
            }
            Console.WriteLine(sb.ToString());
        }

        private static void CheckSlogan(string message, string wordsStr, ICollection<string> output)
        {
            if (message == string.Empty)
            {
                return;
            }
            var separator = new[] { ' ' };
            var words = wordsStr.Split(separator, StringSplitOptions.RemoveEmptyEntries).ToList();

            CheckSlogan(message, words, new BigList<string>(), output);
        }

        private static void CheckSlogan(string message, IEnumerable<string> words, ICollection<string> preceding, ICollection<string> output)
        {
            if (message == string.Empty)
            {
                output.Add(string.Join(" ", preceding));
                return;
            }

            var isMessageValid = false;
            foreach (var word in words)
            {
                if (message.StartsWith(word))
                {
                    isMessageValid = true;
                    preceding.Add(word);

                    var rest = message.Substring(word.Length);

                    CheckSlogan(rest, words, preceding, output);
                }
            }

            if (!isMessageValid)
            {
                output.Add(NotValid);
            }
        }
    }
}
