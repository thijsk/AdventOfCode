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
			.Where(i => Solve(i.answer, i.values) > 0)
			.Sum(i => i.answer);
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 354060705047464;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		return input.AsParallel()
			.Where(i => Solve2(i.answer, i.values.ToArray()) > 0)
			.Sum(i => i.answer);
	}

	private int Solve(long equationAnswer, long[] equationValues)
	{
		var answers = 0;

		if (equationValues.Length == 1)
		{
			return equationValues[0] == equationAnswer ? 1 : 0;
		}

		var first = equationValues[0];
		var second = equationValues[1];

		answers += Solve(equationAnswer, new[] { first + second }.Concat(equationValues[2..]).ToArray());
		answers += Solve(equationAnswer, new[] { first * second }.Concat(equationValues[2..]).ToArray());

		return answers;
	}

	private int Solve2(long equationAnswer, long[] equationValues)
	{
		var answers = 0;

		if (equationValues.Length == 1)
		{
			return equationValues[0] == equationAnswer ? 1 : 0;
		}

		var first = equationValues[0];
		var second = equationValues[1];

		answers += Solve2(equationAnswer, new[] { first + second }.Concat(equationValues[2..]).ToArray());
		answers += Solve2(equationAnswer, new[] { first * second }.Concat(equationValues[2..]).ToArray());
		answers += Solve2(equationAnswer, new[] { Math2.Concat(first,second) }.Concat(equationValues[2..]).ToArray());

		return answers;
	}


	private (long answer, long[] values) Parse(string line)
	{
		var parts = line.Split(": ");
		return (long.Parse(parts[0]), parts[1].Split(' ').Select(long.Parse).ToArray());
	}

}