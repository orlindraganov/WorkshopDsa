    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace LargestAreaInMatrix
    {
        class LargestArea
        {
            static void Main(string[] args)
            {
                var size = Console.ReadLine().Split().Select(int.Parse).ToArray();

                var rows = size[0];
                var cols = size[1];

                var matrix = new int[rows, cols];
                var visited = new bool[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    var elements = Console.ReadLine().Split().Select(int.Parse).ToArray();

                    for (int j = 0; j < cols; j++)
                    {
                        matrix[i, j] = elements[j];
                    }
                }

                var max = 0;

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        if (visited[i, j])
                        {
                            continue;
                        }
                        var node = new Node(i, j);
                        var count = node.Dfs(matrix, visited);
                        if (count > max)
                        {
                            max = count;
                        }
                    }
                }

                Console.WriteLine(max);
            }

        }

        internal class Node
        {
            public Node(int row, int col)
            {
                this.Row = row;
                this.Col = col;
                this.Neighbours = new List<Node>();
            }

            public int Row { get; set; }
            public int Col { get; set; }
            public IList<Node> Neighbours { get; private set; }

            public int RecursiveBfs(int[,] matrix, bool[,] visited)
            {
                if (visited[this.Row, this.Col])
                {
                    return 0;
                }
                visited[this.Row, this.Col] = true;
                this.GetNeighbours(matrix, visited);

                var count = 1;
                return 1 + this.Neighbours.Sum(n => n.RecursiveBfs(matrix, visited));
            }

            public int IterativeBfs(int[,] matrix, bool[,] visited)
            {
                if (visited[this.Row, this.Col])
                {
                    return 0;
                }
                visited[this.Row, this.Col] = true;

                var stack = new Stack<Node>();
                var count = 0;

                stack.Push(this);

                while (stack.Count > 0)
                {
                    count++;
                    var node = stack.Pop();
                    node.GetNeighbours(matrix, visited);

                    foreach (var neighbour in node.Neighbours)
                    {
                        visited[neighbour.Row, neighbour.Col] = true;
                        stack.Push(neighbour);
                    }
                }

                return count;
            }

            public int Dfs(int[,] matrix, bool[,] visited)
            {
                if (visited[this.Row, this.Col])
                {
                    return 0;
                }
                visited[this.Row, this.Col] = true;

                var queue = new Queue<Node>();
                var count = 0;

                queue.Enqueue(this);

                while (queue.Count > 0)
                {
                    count++;
                    var node = queue.Dequeue();
                    node.GetNeighbours(matrix, visited);

                    foreach (var neighbour in node.Neighbours)
                    {
                        visited[neighbour.Row, neighbour.Col] = true;
                        queue.Enqueue(neighbour);
                    }
                }

                return count;
            }

        public void GetNeighbours(int[,] matrix, bool[,] visited)
            {
                var directions = new int[4, 2] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

                for (int i = 0; i < directions.GetLength(0); i++)
                {
                    var row = this.Row + directions[i, 0];
                    var col = this.Col + directions[i, 1];

                    if (!IsValid(matrix, visited, row, col))
                    {
                        continue;
                    }

                    if (matrix[this.Row, this.Col] == matrix[row, col])
                    {
                        this.Neighbours.Add(new Node(row, col));
                    }
                }
            }

            public bool IsValid(int[,] matrix, bool[,] visited, int row, int col)
            {
                if (row < 0 || row > matrix.GetLength(0) - 1 || col < 0 || col > matrix.GetLength(1) - 1)
                {
                    return false;
                }
                return !visited[row, col];
            }
        }
    }
