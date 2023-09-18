using System.Text;
using Common;

namespace ConsoleApp2016;


public class Day06 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input;

		Dictionary<int, Dictionary<char, int>> frequencies = new();

		foreach (var line in input)
		{
			for (int i = 0; i < line.Length; i++)
			{
				var character = line[i];
				if (!frequencies.ContainsKey(i))
				{
					frequencies[i] = new Dictionary<char, int>();
				}
				if (frequencies[i].ContainsKey(character))
				{
					frequencies[i][character]++;
				}
				else
				{
					frequencies[i][character] = 1;
				}
			}
		}

		var messageBuilder = new StringBuilder();

		foreach (var letter in frequencies.Keys.Select(index => frequencies[index].MaxBy(kvp => kvp.Value).Key))
		{
			messageBuilder.Append(letter);
		}

		var message = messageBuilder.ToString();
		Console.WriteLine(message);
		return 0;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;
		var input = PuzzleContext.Input;

		Dictionary<int, Dictionary<char, int>> frequencies = new();

		foreach (var line in input)
		{
			for (var i = 0; i < line.Length; i++)
			{
				var character = line[i];
				if (!frequencies.ContainsKey(i))
				{
					frequencies[i] = new Dictionary<char, int>();
				}
				if (frequencies[i].ContainsKey(character))
				{
					frequencies[i][character]++;
				}
				else
				{
					frequencies[i][character] = 1;
				}
			}
		}

		var messageBuilder = new StringBuilder();

		foreach (var letter in frequencies.Keys.Select(index => frequencies[index].MinBy(kvp => kvp.Value).Key))
		{
			messageBuilder.Append(letter);
		}

		var message = messageBuilder.ToString();
		Console.WriteLine(message);
		return 0;
	}
}
