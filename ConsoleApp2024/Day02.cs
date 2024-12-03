using Common;

namespace ConsoleApp2024;

public class Day02 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 472;
		PuzzleContext.UseExample = false ;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		long safe = 0;

		var line = 1;
		foreach (var level in input)
		{
			var reverse = level.Reverse().ToArray();

			var ok = isOk(level) || isOk(reverse);
			if (ok)
			{
				Console.WriteLine($"Line {line} is ok");
				safe++;
			}
			else
			{
				Console.WriteLine($"Line {line} is not ok");
			}
			line++;
		}
		return safe;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 520;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();
		long safe = 0;

		foreach (var level in input)
		{
			var reverse = level.Reverse().ToArray();

			var ok = isOk(level) || isOk(reverse);
			if (ok)
			{
				safe++;
			}
			else
			{
				for (int i = 0; i < level.Length; i++)
				{
					var copy = level.ToList();
					copy.RemoveAt(i);
					var reverseCopy = copy.AsEnumerable().Reverse().ToArray();
					var ok2 = isOk(copy.ToArray()) || isOk(reverseCopy);
					if (ok2)
					{
						safe++;
						break;
					}
				}
			}
		}
		
		return safe;
	}

	private static bool isOk(long[] level)
	{
		var ok = true;
		for (var i = 0; i < level.Length - 1; i++)
		{
			var difference = level[i + 1] - level[i];
			if (difference is <= 0 or > 3)
			{
				ok = false;
				break;
			}
		}

		return ok;
	}

	private static long[] Parse(string line)
	{
		return line.Split(' ').Select(long.Parse).ToArray();
	}

}