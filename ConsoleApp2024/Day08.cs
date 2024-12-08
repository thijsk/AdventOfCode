using Common;
using System.ComponentModel;

namespace ConsoleApp2024;

public class Day08 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid();
		var antennas = new Dictionary<char, List<(int x,int y)>>();
		var antinodes = new HashSet<(int x,int y)>();

		foreach ((int x, int y) in input.GetIndexes())
		{
			var c = input[x, y];
			if (c == '.') 
				continue;

			if (!antennas.ContainsKey(c))
				antennas.Add(c, new List<(int x, int y)> { });
			antennas[c].Add((x, y));
		}

		foreach (var a in antennas.Keys)
		{
			var group = antennas[a];

			var combinations = group.Combinations(2).Select(p => p.OrderBy(xy => (xy.x, xy.y)).ToArray());

			foreach (var combination in combinations)
			{
				var first = combination[0];
				var second = combination[1];

				var aone = second.Add(second.Subtract(first));
				var atwo = first.Subtract(second.Subtract(first));

				if (input.IsInGrid(aone))
					antinodes.Add(aone);
				if (input.IsInGrid(atwo))
					antinodes.Add(atwo);
			}
		}

		Print(input, antinodes);

		return antinodes.Count;
	}

	private void Print(char[,] grid, HashSet<(int x, int y)> antinodes)
	{
		var rows = grid.GetLength(0);
		var columns = grid.GetLength(1);

		Console.WriteLine();
		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < columns; c++)
			{
				if (antinodes.Contains((r, c)))
					Console.Write('#');
				else
					Console.Write(grid[r, c]);
			}

			Console.WriteLine();
		}
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid();
		var antennas = new Dictionary<char, List<(int x, int y)>>();
		var antinodes = new HashSet<(int x, int y)>();

		foreach ((int x, int y) in input.GetIndexes())
		{
			var c = input[x, y];
			if (c == '.')
				continue;

			if (!antennas.ContainsKey(c))
				antennas.Add(c, new List<(int x, int y)> { });
			antennas[c].Add((x, y));
		}

		foreach (var a in antennas.Keys)
		{
			var group = antennas[a];

			var combinations = group.Combinations(2).Select(p => p.OrderBy(xy => (xy.x, xy.y)).ToArray());

			foreach (var combination in combinations)
			{
				var first = combination[0];
				var second = combination[1];

				var diff = second.Subtract(first);
				var aone = second.Add(diff);
				var atwo = first.Subtract(diff);

				while (input.IsInGrid(aone))
				{
					antinodes.Add(aone);
					aone = aone.Add(diff);
				}


				while (input.IsInGrid(atwo))
				{
					antinodes.Add(atwo);
					atwo = atwo.Subtract(diff);
				}

				antinodes.Add(first);
				antinodes.Add(second);
			}
		}

		Print(input, antinodes);

		return antinodes.Count;
	}



}