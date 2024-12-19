using Common;

namespace ConsoleApp2024;

public class Day19 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 278;
		PuzzleContext.UseExample = false;
		
		var (patternInput, designInput) = PuzzleContext.Input.SplitByEmptyLines();
		var patterns = patternInput.First().Split(", ").OrderByDescending(p => p.Length).ToArray();
		var designs = designInput.ToArray();

		//var deduplicated = new List<string>();
		//foreach (var pattern in patterns)
		//{
		//	var rex = new Regex($"^({string.Join("|", patterns.Except([pattern]))})+$", RegexOptions.Compiled);
		//	if (!rex.IsMatch(pattern))
		//	{
		//		ConsoleX.WriteLine(pattern);
		//		deduplicated.Add(pattern);
		//	}
		//}

		//var regex = new Regex($"^(({string.Join("|", deduplicated)})+)$", RegexOptions.Compiled);
		//ConsoleX.WriteLine(regex.ToString());

		//var result = 0L;

		//foreach (var design in designs)
		//{
		//	ConsoleX.WriteLine(design);
		//	if (regex.IsMatch(design))
		//	{
		//		result++;
		//	}
		//}



		return designs.Count(d => Solve(d, patterns) > 0);
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 569808947758890;
		PuzzleContext.UseExample = false;

		var (patternInput, designInput) = PuzzleContext.Input.SplitByEmptyLines();
		var patterns = patternInput.First().Split(", ").OrderByDescending(p => p.Length).ToArray();
		var designs = designInput.ToArray();

		return designs.Sum(d => Solve(d, patterns));
	}

	private Dictionary<string,long> cache = new();


	long Solve(string design, string[] patterns)
	{
		if (cache.TryGetValue(design, out var cached))
		{
			return cached;
		}

		if (string.IsNullOrEmpty(design))
		{
			return 1;
		}
		var result = 0L;
		foreach (var pattern in patterns)
		{
			if (design.StartsWith(pattern))
			{
				result += Solve(design[pattern.Length..], patterns);
			}
		}
		cache.Add(design, result);
		return result;
	}

}