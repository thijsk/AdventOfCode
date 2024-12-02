using Common;

namespace ConsoleApp2024;

public class Day01 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 3508942;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse);

		var ll = new List<long>();
		var rl = new List<long>();
		foreach (var (left, right) in input)
		{
			ll.Add(left);
			rl.Add(right);
		}

		ll.Sort();
		rl.Sort();

		long sum = ll.Select((t, i) => Math.Abs(t - rl[i])).Sum();

		return sum;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 26593248;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse);

		var ll = new List<long>();
		var rl = new List<long>();
		foreach (var (left, right) in input)
		{
			ll.Add(left);
			rl.Add(right);
		}

		long sum = 0;
		foreach (var left in ll)
		{
			var count = rl.Count(x => x == left);
			var score = count * left;
			sum += score;
		}

		return sum;
	}

	private (long left, long right) Parse(string line)
	{
		(long left, long right) = line.Split("   ").Select(long.Parse).ToArray();
		return (left, right);
	}

}	