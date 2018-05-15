using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Actions
{
    class Actions
    {
        static void Main()
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var numbersCount = input[0];
            var actionsCount = input[1];

            var nodes = new List<Node<int>>(numbersCount);

            for (int i = 0; i < numbersCount; i++)
            {
                nodes.Add(Node<int>.Parse(i));
            }

            for (int i = 0; i < actionsCount; i++)
            {
                var action = Console.ReadLine().Split().Select(int.Parse).ToArray();

                nodes[action[0]].AddChild(nodes[action[1]]);
            }

            var sb = new StringBuilder();

            var hasVisited = true;

            while (hasVisited)
            {
                hasVisited = false;

                for (int i = 0; i < nodes.Count; i++)
                {
                    var node = nodes[i];
                    if (node.IsFree)
                    {
                        sb.AppendLine(node.ToString());

                        node.GetVisited();
                        hasVisited = true;

                        i = -1;
                    }
                }
            }


            Console.WriteLine(sb.ToString().Trim());
        }
    }

    internal class Node<T>
    {
        public static Node<T> Parse(T t)
        {
            return new Node<T>(t);
        }

        public Node(T value)
        {
            this.Value = value;
            this.ParentsCount = 0;
            this.Children = new HashSet<Node<T>>();
        }

        public T Value { get; private set; }
        public int ParentsCount { get; private set; }
        public ICollection<Node<T>> Children { get; private set; }
        public bool IsFree
        {
            get
            {
                return this.ParentsCount == 0;
            }
        }

        public void AddChild(Node<T> child)
        {
            child.AddParent();
            this.Children.Add(child);
        }

        public void AddParent()
        {
            this.ParentsCount++;
        }

        public void GetVisited()
        {
            foreach (var child in this.Children)
            {
                //this.RemoveChild(child);
                child.RemoveParent();
            }

            this.ParentsCount = -1;
        }

        public void RemoveChild(Node<T> child)
        {
            if (!this.Children.Contains(child))
            {
                throw new ArgumentException("Error dage");
            }

            this.Children.Remove(child);
        }

        public void RemoveParent()
        {
            if (--this.ParentsCount < 0)
            {
                throw new InvalidOperationException("Error dage");
            }
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
