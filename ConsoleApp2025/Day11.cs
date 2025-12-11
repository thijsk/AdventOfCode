using Common;

namespace ConsoleApp2025;

public class Day11 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 758;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToDictionary(kv => kv.key, kv => kv.values);

		var totalOut = 0L;

		totalOut = CountOut("you", "out", input);

		return totalOut;
	}

	Dictionary<string, long> memory = new();

	private long CountOut(string from, string end, Dictionary<string, List<string>> input)
	{
		if (memory.TryGetValue(from, out var cached))
		{
			return cached;
		}

		if (from == end)
		{
			Console.WriteLine(from);
			return 1;
		}

		var to = input[from];

		var totalOut = 0L;
		foreach (var t in to)
		{
			totalOut += CountOut(t, end, input);
		}

		memory.Add(from, totalOut);

		return totalOut;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToDictionary(kv => kv.key, kv => kv.values);
		input.Add("out", new List<string>());

		memory.Clear();
		var countFft = CountOut("svr", "fft", input);

		memory.Clear();
		var countDac = CountOut("fft", "dac", input);

		memory.Clear();
		var countOut = CountOut("dac", "out", input);

		Console.WriteLine($"Fft: {countFft}, Dac: {countDac}, Out: {countOut}");

		return countFft * countDac * countOut;
	}

	private (string key, List<string> values) Parse(string line)
	{
		var parts = line.Split(':');
		var key = parts[0].Trim();
		var values = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()).ToList();
		return (key, values);
	}

}