using Common;

namespace ConsoleApp2024;

public class Day06 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 5318;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid(c => c);

		var visited = Solve(input);

		return visited.Count;
	}

	static bool printGrid = false;
	static (int x, int y) highlight = (-1, -1);

	public long Part2()
	{
		PuzzleContext.Answer2 = 1831;
		PuzzleContext.UseExample = false;
		var input = PuzzleContext.Input.GetGrid(c => c);
		var visited = Solve(input);

		long options = visited.Where(v => input[v.x, v.y] == '.')
			.AsParallel()
			.Sum(v => GetsStuck(input, v) ? 1 : 0);

		return options;
	}

	private static HashSet<(int x, int y)> Solve(char[,] input)
	{
		var guard = input.Find('^').First();
		var facing = Directions.Up;

		HashSet<(int x, int y)> visited = [ guard ];

		while (true)
		{
			var next = input.GetNeighborInDirection(guard, facing);

			if (!next.HasValue)
				break;

			if (input[next.Value.x, next.Value.y] == '#')
			{
				facing = Directions.TurnRight(facing);
				continue;
			}

			guard = next.Value;

			visited.Add(guard);
		}

		return visited;
	}

	private static bool GetsStuck(char[,] input, (int x, int y) block)
	{
		var guard = input.Find('^').First();
		var facing = Directions.Up;

		HashSet<((int x, int y), (int x, int y))> visited = [ (guard, facing) ];
		while (true)
		{
			var next = input.GetNeighborInDirection(guard, facing);

			if (!next.HasValue)
			{
				//highlight = guard;
				//PrintGrid(input, visited);
				return false;
			}

			if (input[next.Value.x, next.Value.y] == '#' || next.Value == block)
			{
				facing = Directions.TurnRight(facing);
				continue;
			}

			guard = next.Value;

			if (!visited.Add((guard,facing)))
			{
				highlight = guard;
				PrintGrid(input, visited);
				return true;
			}
		}
	}

	public static void PrintGrid(char[,] grid, HashSet<((int x, int y), (int x, int y))> visited)
	{
		if (!printGrid)
			return;
		var rows = grid.GetLength(0);
		var columns = grid.GetLength(1);


		Console.WriteLine();
		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < columns; c++)
			{
				if ((r, c) == highlight)
				{
					Console.BackgroundColor = ConsoleColor.Yellow;
					Console.ForegroundColor = ConsoleColor.Red;
				}

				if (visited.Contains(((r, c), Directions.Up)))
					Console.Write('^');
				else if (visited.Contains(((r, c), Directions.Down)))
					Console.Write('v');
				else if (visited.Contains(((r, c), Directions.Left)))
					Console.Write('<');
				else if (visited.Contains(((r, c), Directions.Right)))
					Console.Write('>');
				else
					Console.Write(grid[r, c]);

				Console.BackgroundColor = ConsoleColor.Black;
				Console.ForegroundColor = ConsoleColor.White;
			}

			Console.WriteLine();
		}
	}

}