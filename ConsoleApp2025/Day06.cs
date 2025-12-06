using Common;

namespace ConsoleApp2025;

public class Day06 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var numberOfProblems = input[0].Length;
		var numberOfTerms = input.Length - 1;

		var terms = input[0..^1];

		var result = 0L;

		for (int p = 0; p < numberOfProblems ; p++)
		{
			var operand = input[^1][p];

			var presult = operand == "*" ? 1L : 0L;

			foreach (var t in terms)
			{
				if (operand == "*")
					presult *= long.Parse(t[p]);
				else if (operand == "+")
					presult += long.Parse(t[p]);
			}

			Console.WriteLine(presult);

			result+=presult;
				//else if (operand == "+")
				//	result += input[0..^2][p].Select(long.Parse).Aggregate(1L, (n1, n2) => n1 + n2);
			//for (int t = 0; t < numberOfTerms; t++)
			//{
			//	input[t][p].Select(long.Parse).
			//}
		}

		return result;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input;

		var nc = input[0].Length;
		var nr = input.Length;

		var result = 0L;

		var problems = new List<string>();
		for (int c = nc - 1; c >= 0; c--)
		{
			var rstr = "";
			for (int r = 0; r < nr; r++)
			{
				rstr += input[r][c];
			}
			problems.Add(rstr.Trim());
		}

		List<long> terms = new();
		var outcome = 0L;

		foreach (var problem in problems)
		{
		

			if (problem.Length == 0)
				continue;

			var lastChr = problem[^1];

			if (char.IsNumber(lastChr))
			{
				terms.Add(long.Parse(problem));
			} else
			{
				terms.Add(long.Parse(problem[0..^1]));

				var operand = lastChr;

				if (operand == '+')
				{
				   outcome	= terms.Sum();
				}
				else
				{
					outcome = terms.Aggregate(1L, (n1, n2) => n1 * n2);
				}

				result += outcome;
				Console.WriteLine(outcome);

				terms.Clear();
			}

		}

		return result;
	}

	private string[] Parse(string line)
	{
		return line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
	}

}