using Common;

namespace ConsoleApp2023;

public class Day01 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 55607;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		return input.Sum();
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 55291;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse2).ToArray();

		foreach (var val in input)
			Console.WriteLine(val);

		return input.Sum();
	}

	public long Parse(string line)
	{
		var digits = line.Where(c => char.IsDigit(c));
		return long.Parse(string.Join("", digits.First(), digits.Last()));
	}

	public long Parse2(string line)
	{
        Console.WriteLine(line); 
        var indexValues = new Dictionary<int, int>();

        for (var i = 1; i <= 9; i++)
        {
	        var indexes = line.AllIndexesOf(strDigits[i-1])
		        .Concat(line.AllIndexesOf(i.ToString()));
	        foreach (var index in indexes)
	        {
		        indexValues.Add(index, i);
	        }
        }
		
		var firstDigit = indexValues[indexValues.Keys.Min()];
		var lastDigit = indexValues[indexValues.Keys.Max()];
		return long.Parse(string.Join("", firstDigit,lastDigit));
	}

	readonly string[] strDigits = {
		"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
	};

}