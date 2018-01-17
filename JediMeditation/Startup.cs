using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JediMeditation
{
    internal class Startup
    {
        private static void Main()
        {
            var numberOfJedi = int.Parse(Console.ReadLine());

            var jedisStr = Console.ReadLine();

            var masters = new Queue<string>();
            var knights = new Queue<string>();
            var padawans = new Queue<string>();


            foreach (var jedi in jedisStr.Split())
            {
                switch (jedi[0])
                {
                    case 'M':
                        masters.Enqueue(jedi);
                        break;
                    case 'K':
                        knights.Enqueue(jedi);
                        break;
                    case 'P':
                        padawans.Enqueue(jedi);
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            var sb = new StringBuilder(jedisStr.Length);

            while (masters.Count > 0)
            {
                sb.Append(masters.Dequeue() + " ");
            }
            while (knights.Count > 0)
            {
                sb.Append(knights.Dequeue() + " ");
            }
            while (padawans.Count > 0)
            {
                sb.Append(padawans.Dequeue());
            }

            Console.WriteLine(sb.ToString().Trim());
        }
    }
}
