using Common;
using System;

namespace ConsoleApp2025;

public class Day05 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 0;
		PuzzleContext.UseExample = false;

		var (ranges, ingredients) = Parse(PuzzleContext.Input);

		return ingredients.Count(i => ranges.Any(r => r.start <= i && r.end >= i));
	}		

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var (ranges, ingredients) = Parse(PuzzleContext.Input);

		var last = 0L;
		var result = new List<(long start, long end)>();
		foreach (var range in ranges.OrderBy(r => r.start))
		{
			if (range.start <= last)
			{
				if (range.end > last)
				{
					var newlast = result[^1];
					newlast.end = range.end;
					result[^1] = newlast;
					last = newlast.end;
				}
			}
			else
			{
				result.Add(range);
				last = range.end;
			}
		}

		return result.Sum(r => (r.end - r.start) + 1);
	}

	private ((long start, long end)[] ranges, long[] ingredients) Parse(string[] lines)
	{
		var (ranges, ingredients) = lines.SplitByEmptyLines();

		var parsedRanges = ranges.Select(r =>
		{
			var split = r.Split('-').Select(long.Parse).ToArray();
			return (split[0], split[1]);
		}).ToArray();

		var parsedIngredients = ingredients.Select(long.Parse).ToArray();

		return (parsedRanges, parsedIngredients);

	}

}