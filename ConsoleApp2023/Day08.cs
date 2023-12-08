using Common;

namespace ConsoleApp2023;

public class Day08 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 0;
		PuzzleContext.UseExample = false;

		var input = Parse(PuzzleContext.Input);

		var steps = CountSteps(input, "AAA", d => d == "ZZZ");

		return steps;
	}

	private static long CountSteps((char[] instructions, Dictionary<string, (string left, string right)> map) input, string position, Func<string,bool> destinationFound)
	{
		var step = 0L;
		foreach (var instruction in input.instructions.AsCircular())
		{
			if (destinationFound(position))
			{
				return step;
			}

			step++;

			var (left, right) = input.map[position];

			position = instruction == 'L' ? left : right;
		}

		return step;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = Parse(PuzzleContext.Input);

		var step = 0L;

		var positions = input.map.Keys.Where(key => key[2] == 'A').ToArray();
		var steps = positions.Select(position => CountSteps(input, position, d => d[2] == 'Z')).ToArray();

		return Math2.LeastCommonMultiple(steps);
	}

	public (char[] instructions, Dictionary<string, (string left,string right)> map) Parse(string[] lines)
	{
		var (instructions, map) = lines.SplitByEmptyLines();


		return (instructions.First().ToArray(), map.ToDictionary(line => SplitKey(line), line => SplitLeftRight(line)));
	}

	public string SplitKey(string line)
	{
		return line.Split('=', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0];
	}

	public (string left, string right) SplitLeftRight(string line)
	{
		var (left, right) = line.Split('=')[1].Replace("(", "").Replace(")", "").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
		return (left, right);
	}

}