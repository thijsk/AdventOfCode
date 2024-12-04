using Common;

namespace ConsoleApp2024;

public class Day04 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 2534;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid(c => c);

		long count = 0;

		for (var x = 0; x < input.GetRowCount(); x++)
		{
			for (var y = 0; y < input.GetColumnCount(); y++)
			{
				if (input[x, y] != 'X') continue;
				foreach (var (dx, dy) in Directions.All)
				{
					var foundM = input.GetNeighborsInDirection((x, y), (dx, dy)).Any(letterM => input[letterM.x, letterM.y] == 'M');
					if (!foundM)
						continue;
					var foundA = input.GetNeighborsInDirection((x + dx, y + dy), (dx, dy)).Any(letterA => input[letterA.x, letterA.y] == 'A');
					if (!foundA)
						continue;
					var foundS = input.GetNeighborsInDirection((x + 2 * dx, y + 2 * dy), (dx, dy)).Any(letterS => input[letterS.x, letterS.y] == 'S');
					if (!foundS)
						continue;
					count++;
				}
			}
		}
		return count;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 1866;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid(c => c);

		long count = 0;

		for (var x = 0; x < input.GetRowCount(); x++)
		{
			for (var y = 0; y < input.GetColumnCount(); y++)
			{
				if (input[x, y] != 'A') continue;
				var n = input.GetNeighborsDiagonal(x, y);

				var m = n.Where(letter => input[letter.x, letter.y] == 'M').ToArray();
				var s = n.Where(letter => input[letter.x, letter.y] == 'S').ToArray();
				if (m.Length != 2 || s.Length != 2) continue;
				if (m[0].y != m[1].y && m[0].x != m[1].x) continue;
				count++;
			}
		}
		return count;
	}
}