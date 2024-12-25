using Common;

namespace ConsoleApp2024;

public class Day25 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 3307;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.SplitByEmptyLines().Select(Parse).ToArray();

		var keys = new List<int[]>();
		var locks = new List<int[]>();

		foreach (var grid in input)
		{
			var gridcolumns= grid.GetColumns();
			var columns = new List<int>();
			var isLock = false;
			foreach (var column in gridcolumns)
			{
				if (column[0] == '#')
				{
					isLock = true;
				}
				columns.Add(column.Count(c => c == '#')-1);
			}
			var list = isLock ? locks : keys;
			list.Add(columns.ToArray());
		}


		return locks.SelectMany(l => keys, (l, k) => (l, k)).Count(pair => Enumerable.Select(pair.k, (p, i) => (p, i)).All((p) => pair.l[p.i] + p.p <= 5));
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