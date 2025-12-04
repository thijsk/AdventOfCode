using Common;

namespace ConsoleApp2025;

public class Day04 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 1460;
		PuzzleContext.UseExample = false;

		var grid = Parse(PuzzleContext.Input);

		var result = 0L;

		char[,]? output = RemoveRolls(grid, ref result);


		return result;
	}

	private static char[,] RemoveRolls(char[,] grid, ref long result)
	{
		var output = (char[,])grid.Clone();
		foreach (var (x, y) in grid.GetIndexes())
		{
			if (grid[x, y] != '@')
				continue;
			var neighbors = grid.GetAllNeighbors(x, y);
			var rolls = neighbors.Count(n => grid[n.x, n.y] == '@');
			if (rolls < 4)
			{
				output[x, y] = 'X';
				result++;
			}
		}

		output.ToConsole();

		return output;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 9243;
		PuzzleContext.UseExample = false;

		var grid = Parse(PuzzleContext.Input);

		var result = 0L;

		var before = 0L;

		do
		{
			before = result;
			grid = RemoveRolls(grid, ref result);

			grid.ToConsole();

		} while (before != result);

		return result;
	}

	private char[,] Parse(string[] lines)
	{
		return lines.GetGrid();
	}

}