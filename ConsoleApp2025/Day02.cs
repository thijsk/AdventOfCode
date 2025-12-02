using Common;

namespace ConsoleApp2025;

public class Day02 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 43952536386;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.SelectMany(Parse);


		Console.WriteLine("start");

		var result = 0L;
		HashSet<long> found = new();
		foreach (var (r1, r2) in input)
		{
			// find ranges where any of numbers have repeating digits
			for (var n = r1; n <= r2; n++)
			{
				if (IsRepeatingPattern(n, 2))
				{
					Console.WriteLine($"Found repeating pattern: {n}");
					if (!found.Add(n))
					{
						Console.WriteLine($"Already found: {n}");
					}
					result += n;
				}
			}
		}

		return result;
	}

	private bool IsRepeatingPattern(long r1, int splits)
	{
		var rstring = r1.ToString();
		if (rstring.Length % splits != 0)
		{
			return false;
		}
		var period = rstring.Length / splits;


		var pattern = rstring.Substring(0, period);
		for (var i = 1; i < splits; i++)
		{
			var part = rstring.Substring(i * period, period);
			if (part != pattern)
			{
				return false;
			}
		}

		return true;

	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.SelectMany(Parse);

		Console.WriteLine("start");

		var result = 0L;
		HashSet<long> found = new();
		foreach (var (r1, r2) in input)
		{
			for (var n = r1; n <= r2; n++)
			{
				for (int l = 2; l <= 10; l++)
				{
					if (IsRepeatingPattern(n, l))
					{
						Console.WriteLine($"Found repeating pattern: {n}");
						if (!found.Add(n))
						{
							Console.WriteLine($"Already found: {n}");
						}
						else
							result += n;
					}
				}
			}
		}

		Console.WriteLine(found.Sum());
		return result;
	}

	private IEnumerable<(long, long)> Parse(string line)
	{
		var parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
		foreach (var part in parts)
		{
			Console.WriteLine($"Parsing part: {part}");
			var rangeParts = part.Split('-');
			yield return (long.Parse(rangeParts[0]), long.Parse(rangeParts[1]));
		}
	}

}