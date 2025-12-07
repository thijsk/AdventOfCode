using Common;

namespace ConsoleApp2025;

public class Day07 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 1507;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid();

		var count = 0L;

		for (int r = 0; r < input.GetRowCount(); r++)
		{
			for (int c = 0; c < input.GetColumnCount(); c++)
			{
				var cell = input[r, c];
				
				if (r == 0)
				{
					if (cell == 'S')
					{
						input[r, c] = '|';
					}
					
				} else
				{
					var above = input[r - 1, c];
					if (above == '|')
					{
						if (cell == '^')
						{
							input[r, c - 1] = '|';
							input[r, c + 1] = '|';
							count++;

						} else if (cell == '.')
						{
							input[r, c] = '|';
						}
					}
				}
			}
		}


		input.ToConsole();

		return count;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 1537373473728;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid();


		var s = (0, input.GetRow(0).IndexOf('S'));
		var count = GetCount(input, s, input.GetRowCount()-1);

		return count;
	}

	private Dictionary<(int, int), long> cache = new();

	private long GetCount(char[,] input, (int r, int c) s, int maxr)
	{
		if (s.r == maxr)
			return 1;

		long result;
		if (cache.TryGetValue(s, out result))
		{
			return result;
		}

		if (input[s.r, s.c] == '^')
		{
			var left = GetCount(input, (s.r+1, s.c- 1), maxr);
			var right = GetCount(input, (s.r+1, s.c+ 1), maxr);
			cache.Add(s, left + right);
			return left + right;
		}
		else
		{
			var below = GetCount(input, (s.r + 1, s.c), maxr);
			cache.Add(s, below);
			return below;
		}
	}
}