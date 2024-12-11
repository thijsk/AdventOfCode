using Common;

namespace ConsoleApp2024;

public class Day11 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 193269;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).First();

		return input.Sum(s => BlinkCount(s, 25));
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 228449040027793;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).First();

		return input.Sum(s => BlinkCount(s, 75));
	}

	Dictionary<(long stone, int iteration), long> countCache = [];

	private long BlinkCount(long value, int iteration)
	{
		if (iteration == 0)
			return 1;

		if (countCache.TryGetValue((value, iteration), out var result))
			return result;

		var next = BlinkStone(value);
		var count = next.Sum(s => BlinkCount(s, iteration - 1));
		return countCache[(value, iteration)] = count;
	}

	Dictionary<long, long[]> stoneCache = [];

	private long[] BlinkStone(long stone)
	{
		if (stoneCache.TryGetValue(stone, out var blinkStone))
			return blinkStone;

		long[] result;
		if (stone == 0)
			result = [1];
		else if (stone.NumberOfDigits().IsEven())
		{
			result = SplitDigits(stone);
		}
		else
		{
			result = [stone * 2024];
		}

		return stoneCache[stone] = result;
	}

	private static long[] SplitDigits(long stone)
	{
		var halfway = Math2.Pow(10, stone.NumberOfDigits() / 2);
		return [stone / halfway, stone % halfway];
	}

	private long[] Parse(string line)
	{
		return line.Split(' ').Select(long.Parse).ToArray();
	}

}