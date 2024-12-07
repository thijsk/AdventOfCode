using Common;

namespace ConsoleApp2024;

public class Day07 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 20665830408335;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		return input.AsParallel()
			.Where(i => Solve(i.answer, i.values.ToList()) > 0)
			.Sum(i => i.answer);
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 354060705047464;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		return input.AsParallel()
			.Where(i => Solve2(i.answer, i.values.ToList()) > 0)
			.Sum(i => i.answer);
	}

	private int Solve(long equationAnswer, List<long> equationValues)
	{
		var answers = 0;

		var first = equationValues[0];
		var second = equationValues[1];

		var sum = first + second;
		var multiple = first * second;

		var remaining = equationValues.Skip(2).ToList();
		if (remaining.Any())
		{
			var sumremaining = new List<long> { sum };
			sumremaining.AddRange(remaining);
			answers += Solve(equationAnswer, sumremaining);

			var multipleremaining = new List<long> { multiple };
			multipleremaining.AddRange(remaining);
			answers += Solve(equationAnswer, multipleremaining);
		}
		else
		{
			if (sum == equationAnswer)
			{
				answers++;
			}
			if (multiple == equationAnswer)
			{
				answers++;
			}
		}

		return answers;
	}

	static long fastconcat(long a, long b)
	{
		if (b < 0L) { throw new ArgumentOutOfRangeException("b", "b must be greater than or equal to 0"); }
		if (b < 10L) return 10L * a + b;
		if (b < 100L) return 100L * a + b;
		if (b < 1000L) return 1000L * a + b;
		if (b < 10000L) return 10000L * a + b;
		if (b < 100000L) return 100000L * a + b;
		if (b < 1000000L) return 1000000L * a + b;
		if (b < 10000000L) return 10000000L * a + b;
		if (b < 100000000L) return 100000000L * a + b;
		return long.Parse(a.ToString() + b.ToString());
	}

	private int Solve2(long equationAnswer, List<long> equationValues)
	{
		var answers = 0;

		var first = equationValues[0];
		var second = equationValues[1];

		var sum = first + second;
		var multiple = first * second;
		var concat = fastconcat(first, second);

		if (equationValues.Count > 2)
		{
			var remaining = equationValues.Skip(2).ToList();

			var sumremaining = new List<long> { sum };
			sumremaining.AddRange(remaining);
			answers += Solve2(equationAnswer, sumremaining);

			var multipleremaining = new List<long> { multiple };
			multipleremaining.AddRange(remaining);
			answers += Solve2(equationAnswer, multipleremaining);

			var concatremaining = new List<long> { concat };
			concatremaining.AddRange(remaining);
			answers += Solve2(equationAnswer, concatremaining);
		}
		else
		{
			if (sum == equationAnswer)
			{
				answers++;
			}
			if (multiple == equationAnswer)
			{
				answers++;
			}
			if (concat == equationAnswer)
			{
				answers++;
			}
		}

		return answers;
	}


	private (long answer, long[] values) Parse(string line)
	{
		var parts = line.Split(": ");
		return (long.Parse(parts[0]), parts[1].Split(' ').Select(long.Parse).ToArray());
	}

}