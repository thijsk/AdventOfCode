using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class ArrayExtensions
    {
        public static T[][] GetRows<T>(this T[,] input)
        {
            return Enumerable.Range(input.GetLowerBound(0), input.GetUpperBound(0) + 1)
                .Select(row => Enumerable.Range(input.GetLowerBound(1), input.GetUpperBound(1) + 1).Select(col => input[row, col]).ToArray()).ToArray();
        }

        public static T[] GetRow<T>(this T[,] input, int row)
        {
            return Enumerable.Range(input.GetLowerBound(1), input.GetUpperBound(1) + 1).Select(col => input[row, col])
                .ToArray();
        }

        public static (int x, int y)[] GetRowIndexes<T>(this T[,] input, int row)
        {
            return Enumerable.Range(input.GetLowerBound(1), input.GetUpperBound(1) + 1).Select(col => (row, col))
                .ToArray();
        }

        public static T[][] GetColumns<T>(this T[,] input)
        {
            return Enumerable.Range(input.GetLowerBound(1), input.GetUpperBound(1) + 1)
                .Select(col => Enumerable.Range(input.GetLowerBound(0), input.GetUpperBound(0) + 1).Select(row => input[row, col]).ToArray()).ToArray();
        }

        public static T[] GetColumn<T>(this T[,] input, int col)
        {
            return Enumerable.Range(input.GetLowerBound(0), input.GetUpperBound(0) + 1)
                .Select(row => input[row, col]).ToArray();
        }

        public static (int x, int y)[] GetColumnIndexes<T>(this T[,] input, int col)
        {
            return Enumerable.Range(input.GetLowerBound(0), input.GetUpperBound(0) + 1)
                .Select(row => (row, col)).ToArray();
        }

        public static (int x, int y)[] GetIndexes<T>(this T[,] input)
        {
            return Enumerable.Range(input.GetLowerBound(0), input.GetUpperBound(0) + 1)
                .SelectMany(row => Enumerable.Range(input.GetLowerBound(1), input.GetUpperBound(1) + 1).Select(col => (row, col))).ToArray();
        }

        public static int GetRowCount<T>(this T[,] input)
        {
            return input.GetUpperBound(0) + 1;
        }

        public static int GetColumnCount<T>(this T[,] input)
        {
            return input.GetUpperBound(1) + 1;
        }

        public static IEnumerable<T> SubRange<T>(this T[] input, int start, int end)
        {
            if (start > end)
            {
                return input.SubRange(end, start).Reverse();
            }

            return input.Skip(start).Take((end - start) + 1);
        }

        public static void ToConsole<T>(this T[,] grid)
        {
            grid.ToConsole(o => ConsoleX.Write(o));
        }

        public static void ToConsole<T>(this T[,] grid, Func<T, char> convert)
        {
	        grid.ToConsole(o => ConsoleX.Write(convert(o)));
        }

		public static void ToConsole<T>(this T[,] grid, Action<T> write)
        {
            for (var row = 0; row <= grid.GetUpperBound(0); row++)
            {
                for (var col = 0; col <= grid.GetUpperBound(1); col++)
                {
                   write(grid[row, col]);
                }

                ConsoleX.WriteLine();
            }
        }

        public static void ToConsole<T>(this T[,] grid, Action<(int x, int y), T> write)
        {
            for (var row = 0; row <= grid.GetUpperBound(0); row++)
            {
                for (var col = 0; col <= grid.GetUpperBound(1); col++)
                {
                    write((row, col), grid[row, col]);
                }

                ConsoleX.WriteLine();
            }
        }

        public static T[,] RotateLeft<T>(this T[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            T[,] result = new T[cols, rows];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result[cols - col - 1, row] = grid[row, col];
                }
            }

            return result;
        }

        public static T[,] RotateRight<T>(this T[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            T[,] result = new T[cols, rows];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result[col, rows - row - 1] = grid[row, col];
                }
            }

            return result;
        }

        public static T[,] FlipHorizontal<T>(this T[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            T[,] result = new T[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result[row, col] = grid[rows - row - 1, col];
                }
            }

            return result;
        }

        public static T[,] FlipVertical<T>(this T[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            T[,] result = new T[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result[row, col] = grid[row, cols - col - 1];
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the horizontal and vertical neighbors of the given index in the grid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grid"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static (int x, int y)[] GetNeighbors<T>(this T[,] grid, (int ix, int iy) index)
        {
            return grid.GetNeighbors(index.ix, index.iy);
        }

        /// <summary>
        /// Returns the horizontal and vertical neighbors of the given x and y indexes in the grid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grid"></param>
        /// <param name="ix"></param>
        /// <param name="iy"></param>
        /// <returns>a list of at most 4 (x,y) points</returns>
        public static (int x, int y)[] GetNeighbors<T>(this T[,] grid, int ix, int iy)
        {
            var minx = Math.Max(ix - 1, 0);
            var maxx = Math.Min(ix + 1, grid.GetUpperBound(0));
            var miny = Math.Max(iy - 1, 0);
            var maxy = Math.Min(iy + 1, grid.GetUpperBound(1));

            var result = new List<(int, int)>();

            for (var x = minx; x <= maxx; x++)
            {
                for (var y = miny; y <= maxy; y++)
                {
                    if (!(x == ix && y == iy) && !(x != ix && y != iy))
                    {
                        result.Add((x, y));
                    }
                }
            }

            return result.ToArray();
        }


        /// <summary>
        /// Returns the diagonal neighbors of the given x and y indexes in the grid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grid"></param>
        /// <param name="index"></param>
        /// <returns>a list of at most 4 (x,y) points</returns>
        public static (int x, int y)[] GetNeighborsDiagonal<T>(this T[,] grid, (int ix, int iy) index)
        {
            return grid.GetNeighborsDiagonal(index.ix, index.ix);
        }

        /// <summary>
        /// Returns the diagonal neighbors of the given x and y indexes in the grid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grid"></param>
        /// <param name="ix"></param>
        /// <param name="iy"></param>
        /// <returns>a list of at most 4 (x,y) points</returns>
        public static (int x, int y)[] GetNeighborsDiagonal<T>(this T[,] grid, int ix, int iy)
        {
            var minx = Math.Max(ix - 1, 0);
            var maxx = Math.Min(ix + 1, grid.GetUpperBound(0));
            var miny = Math.Max(iy - 1, 0);
            var maxy = Math.Min(iy + 1, grid.GetUpperBound(1));

            var result = new List<(int, int)>();

            for (var x = minx; x <= maxx; x++)
            {
                for (var y = miny; y <= maxy; y++)
                {
                    if (ix != x && iy != y)
                    {
                        result.Add((x, y));
                    }
                }
            }

            return result.ToArray();
        }


        /// <summary>
        /// Returns all the horizontal, vertical and diagonal neighbors of the given index in the grid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grid"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static (int x, int y)[] GetAllNeighbors<T>(this T[,] grid, (int ix, int iy) index)
        {
            return grid.GetAllNeighbors(index.ix, index.iy);
        }

        /// <summary>
        /// Returns all the horizontal, vertical and diagonal neighbors of the given x and y indexes in the grid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grid"></param>
        /// <param name="ix"></param>
        /// <param name="iy"></param>
        /// <returns>a list of at most 4 (x,y) points</returns>
        public static (int x, int y)[] GetAllNeighbors<T>(this T[,] grid, int ix, int iy)
        {
            var minx = Math.Max(ix - 1, 0);
            var maxx = Math.Min(ix + 1, grid.GetUpperBound(0));
            var miny = Math.Max(iy - 1, 0);
            var maxy = Math.Min(iy + 1, grid.GetUpperBound(1));

            var result = new List<(int, int)>();

            for (var x = minx; x <= maxx; x++)
            {
                for (var y = miny; y <= maxy; y++)
                {
                    if (!(x == ix && y == iy))
                    {
                        result.Add((x, y));
                    }
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Shortest path finding based on the Dijkstra algorithm
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grid"></param>
        /// <param name="start"></param>
        /// <param name="goal"></param>
        /// <param name="getNeighbors">delegate to determine a list of valid next moves and their associated weights</param>
        /// <returns></returns>
        public static (int x, int y)[] Dijkstra<T>(this T[,] grid, (int x, int y) start, (int x, int y) goal, Func<T[,], (int x, int y), IEnumerable<((int x, int y) point, long weight)>> getNeighbors)
        {
            var path = new List<(int x, int y)>();

            PriorityQueue<(int x, int y), long> frontier = new();

            var xLength = grid.GetLength(0);
            var yLength = grid.GetLength(1);

            var pathWeight = new long[xLength, yLength];

            for (var x = 0; x < xLength; x++)
            {
                for (var y = 0; y < yLength; y++)
                {
                    pathWeight[x, y] = long.MaxValue;
                }
            }

            pathWeight[start.x, start.y] = 0;
            frontier.Enqueue(start, 0);
            var visited = new HashSet<(int, int)>();
            var movemap = new Dictionary<(int, int), (int, int)>();

            while (frontier.Count > 0)
            {
                //move
                var current = frontier.Dequeue();
                visited.Add(current);

                if (current == goal)
                {
                    break;
                }

                // explore
                var neighbors = getNeighbors(grid, current);
                foreach (var neighbor in neighbors.Where(n => !visited.Contains(n.point)))
                {
                    var currentWeigth = pathWeight[current.x, current.y];
                    var neighborPathWeight = currentWeigth + neighbor.weight;

                    if (pathWeight[neighbor.point.x, neighbor.point.y] > neighborPathWeight)
                    {
                        frontier.Enqueue(neighbor.point, neighborPathWeight);

                        pathWeight[neighbor.point.x, neighbor.point.y] = neighborPathWeight;
                        if (movemap.ContainsKey(neighbor.point))
                        {
                            movemap[neighbor.point] = current;
                        }
                        else
                        {
                            movemap.Add(neighbor.point, current);
                        }
                    }
                }
            }

            // Backtrack
            var prev = goal;
            if (movemap.ContainsKey(prev))
            {
                while (prev != start)
                {
                    path.Add(prev);
                    prev = movemap[prev];
                }
            }

            return path.ToArray();

        }

        /// <summary>
        /// Shortest path finding based on the Dijkstra algorithm
        /// </summary>
        /// <typeparam name="TCell">Type of a single cell</typeparam>
        /// <typeparam name="TStep">State of a step</typeparam>
        /// <param name="grid"></param>
        /// <param name="start"></param>
        /// <param name="getNeighbors">delegate to determine the set of valid neighbors and their associated weights</param>
        /// <param name="isGoal">delegate to determine if the goal is reached</param>
        /// <returns>path taken and total weight on that path</returns>
        public static (TStep[] path, long weight) Dijkstra<TCell, TStep>(this TCell[,] grid, TStep start,
            Func<TCell[,], TStep, IEnumerable<(TStep point, long weight)>> getNeighbors, Func<TStep, bool> isGoal)
        {
            PriorityQueue<TStep, long> frontier = new();
            Dictionary<TStep, long> pathWeight = new();
            HashSet<TStep> visited = new();
            Dictionary<TStep, TStep> moveMap = new();
            List<TStep> path = new();
            TStep goal = default;

            frontier.Enqueue(start, 0);

            while (frontier.Count > 0)
            {
                //move
                var current = frontier.Dequeue();
                visited.Add(current);

                if (isGoal(current))
                {
                    goal = current;
                    break;
                }

                // explore
                var neighbors = getNeighbors(grid, current);
                foreach (var neighbor in neighbors.Where(n => !visited.Contains(n.point)))
                {
                    var currentWeigth = pathWeight.TryGetValue(current, out var weight) ? weight : 0;
                    var neighborPathWeight = currentWeigth + neighbor.weight;

                    var oldNeighborWeight = pathWeight.TryGetValue(neighbor.point, out var oldWeight)
                        ? oldWeight
                        : long.MaxValue;

                    if (oldNeighborWeight > neighborPathWeight)
                    {
                        frontier.Enqueue(neighbor.point, neighborPathWeight);

                        pathWeight.AddOrSet(neighbor.point, neighborPathWeight);
                        moveMap.AddOrSet(neighbor.point, current);
                    }
                }
            }

            if (!isGoal(goal)) 
                throw new Exception("No path found");

            // Backtrack
            var prev = goal;
            if (moveMap.ContainsKey(prev))
            {
                while (!prev.Equals(start))
                {
                    path.Add(prev);
                    prev = moveMap[prev];
                }
            }

            return (path.ToArray(), pathWeight[goal]);
        }

        /// <summary>
        /// Finds the coordinates of a given value in a grid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grid"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static IEnumerable<(int x, int y)> Find<T>(this T[,] grid, T search) where T : IEquatable<T>
        {
            var result = new List<(int, int)>();

            for (var x = 0; x <= grid.GetUpperBound(0); x++)
            {
                for (var y = 0; y <= grid.GetUpperBound(1); y++)
                {
                    if (grid[x, y].Equals(search))
                    {
                        result.Add((x, y));
                    }
                }
            }

            return result;
        }

        public static bool IsInGrid<T>(this T[,] grid, (int x, int y) point)
        {
            return point.x >= 0 && point.x <= grid.GetUpperBound(0) 
                && point.y >= 0 && point.y <= grid.GetUpperBound(1);
        }

        public static int GetHashCode<T>(this T[,] grid)
        {
            HashCode hashCode = new HashCode();
            foreach (var item in grid)
            {
                hashCode.Add(item);
            }
            return hashCode.ToHashCode();
        }


        public static string ToSingleString<T>(this T[,] array)
        {
            StringBuilder stringBuilder = new();
            foreach (var item in array)
            {
                stringBuilder.Append(item);
            }
            return stringBuilder.ToString();
        }

        public static void Deconstruct<T>(this T[] array, out T s1, out T s2)
        {
            s1 = array.ElementAtOrDefault(0);
            s2 = array.ElementAtOrDefault(1);
        }

        public static void Deconstruct<T>(this T[] array, out T s1, out T s2, out T s3)
        {
            s1 = array.ElementAtOrDefault(0);
            s2 = array.ElementAtOrDefault(1);
            s3 = array.ElementAtOrDefault(2);
        }
    }
}
