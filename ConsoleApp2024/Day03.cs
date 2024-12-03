using Common;
using System.Text.RegularExpressions;

namespace ConsoleApp2024;

public class Day03 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 161085926;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse1).Sum();

		return input;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 82045421;
		PuzzleContext.UseExample = false;

		var lines = String.Join(String.Empty, PuzzleContext.Input);
		var result = Parse2(lines);

		return result;
	}

	private long Parse1(string line)
	{
		var matches = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)").Matches(line);

		return matches.Sum(m => long.Parse(m.Groups[1].Value) * long.Parse(m.Groups[2].Value));
	}


	private long Parse2(string line)
	{
		var mulMatches = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)").Matches(line);
		var doMatches = new Regex(@"do\(\)").Matches(line);
		var dontMatches = new Regex(@"don't\(\)").Matches(line);

		bool enabled = true;
		long sum = 0; 
		for (var index = 0; index <= line.Length; index++)
		{
			var mulMatch = mulMatches.FirstOrDefault(m => m.Index == index);
		    if (mulMatch != null && enabled)
			{
				sum += long.Parse(mulMatch.Groups[1].Value) * long.Parse(mulMatch.Groups[2].Value);
			}

			var doMatch = doMatches.FirstOrDefault(m => m.Index == index);
			if (doMatch != null)
			{
				enabled = true;
			}

			var dontMatch = dontMatches.FirstOrDefault(m => m.Index == index);
			if (dontMatch != null)
			{
				enabled = false;
			}
		}

		return sum;
	}
}