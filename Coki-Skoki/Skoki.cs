//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Coki_Skoki
//{
//    internal class Skoki
//    {
//        private static void Main()
//        {
//            var buildingsCount = int.Parse(Console.ReadLine());

//            char[] separator = { ' ' };

//            var buildings = Console.ReadLine().Split(separator).Select(int.Parse).ToArray();

//            var jumpsFromBuilding = new int[buildingsCount];

//            var sb = new StringBuilder();

//            var max = 0;

//            for (int i = 0; i < buildingsCount; i++)
//            {
//                var currentIndex = buildingsCount - 1 - i;

//                var lastNumber = buildings[currentIndex];

//                for (int j = currentIndex; j < buildingsCount; j++)
//                {
//                    var currentNumber = buildings[j];

//                    if (currentNumber <= lastNumber)
//                    {
//                        continue;
//                    }

//                    var currentCount = 1 + jumpsFromBuilding[j];
//                    jumpsFromBuilding[currentIndex] = currentCount;

//                    if (currentCount > max)
//                    {
//                        max = currentCount;
//                    }

//                    break;
//                }
//                sb.Insert(0, jumpsFromBuilding[currentIndex] + " ");
//            }

//            Console.WriteLine(max);
//            Console.WriteLine(sb.ToString().Trim());
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;

namespace _08_CokiSkoki_II
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var buildings = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var answer = new int[buildings.Length];
            var stackIndexes = new Stack<int>();

            for (int i = buildings.Length - 1; i >= 0; i--)
            {
                var current = buildings[i];

                while (stackIndexes.Count > 0 && current >= buildings[stackIndexes.Peek()])
                {
                    stackIndexes.Pop();
                }

                if (stackIndexes.Count > 0)
                {
                    answer[i] = answer[stackIndexes.Peek()] + 1;
                }

                stackIndexes.Push(i);
            }
            Console.WriteLine("{0}\n{1}", answer.Max(), string.Join(" ", answer));
        }
    }
}


