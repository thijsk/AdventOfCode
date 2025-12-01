using Common;

namespace ConsoleApp2025;

public class Day01 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 1105;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var dial = 50;
		var password = 0L;

		foreach ((var direction, int amount) in input)
		{
			if (direction == 'R')
			{
				dial += amount;
			}
			else
			{
				dial -= amount;

			}
			dial %= 100;

			if (dial == 0)
				password++;
		}

		return password;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 6599;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var dial = 50;
		var password = 0L;

		foreach ((var direction, int amount) in input)
		{
			var step = (direction == 'R') ? 1 : -1;
			for (int i = 0; i < amount; i++)
			{
				dial += step;
				if (step == 1 && dial == 100)
				{
					dial = 0;
				}
				else if (step == -1 && dial == -1)
				{
					dial = 99;
				}
				if (dial == 0)
				{
					password++;
				}
			}
		}

		return password;
	}

	private (char direction, int amount) Parse(string line)
	{
		var direction = line[0];
		var amount = int.Parse(line[1..]);

		return (direction, amount);
	}

}