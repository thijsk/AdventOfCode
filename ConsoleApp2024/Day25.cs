using Common;

namespace ConsoleApp2024;

public class Day25 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.SplitByEmptyLines().Select(Parse).ToArray();

		var keys = new List<(char[,], int[])>();
		var locks = new List<(char[,], int[])>();

		foreach (var grid in input)
		{
			var gridcolumns= grid.GetColumns();
			var columns = new List<int>();
			bool isLock = false;
			foreach (var column in gridcolumns)
			{
				if (column[0] == '#')
				{
					isLock = true;
				}
				columns.Add(column.Count(c => c == '#')-1);
			}
			var list = isLock ? locks : keys;
			list.Add((grid, columns.ToArray()));
		}

		var fit = 0L;

		foreach (var @lock in locks)
		{
			foreach (var key in keys)
			{
				ConsoleX.WriteLine($"{string.Join(",",@lock.Item2)} {string.Join(",", key.Item2)}");

				var fits = true;
				for (int c = 0; c < key.Item2.Length; c++)
				{
					if (key.Item2[c] + @lock.Item2[c] > 5)
					{
						fits = false;
					}
				}
				if (fits)
				{
					fit++;
				}
			}
		}

		return fit;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 2024;
		PuzzleContext.UseExample = false;


		ConsoleX.WriteLine("Merry Christmas! 🎄");

		return 2024;
	}

	private char[,] Parse(string[] lines)
	{
		return lines.GetGrid();
	}

}