namespace ConsoleApp2016;

/*
--- Day 1: No Time for a Taxicab ---
Santa's sleigh uses a very high-precision clock to guide its movements, and the clock's oscillator is regulated by stars. Unfortunately, the stars have been stolen... by the Easter Bunny. To save Christmas, Santa needs you to retrieve all fifty stars by December 25th.

Collect stars by solving puzzles. Two puzzles will be made available on each day in the Advent calendar; the second puzzle is unlocked when you complete the first. Each puzzle grants one star. Good luck!

You're airdropped near Easter Bunny Headquarters in a city somewhere. "Near", unfortunately, is as close as you can get - the instructions on the Easter Bunny Recruiting Document the Elves intercepted start here, and nobody had time to work them out further.

The Document indicates that you should start at the given coordinates (where you just landed) and face North. Then, follow the provided sequence: either turn left (L) or right (R) 90 degrees, then walk forward the given number of blocks, ending at a new intersection.

There's no time to follow such ridiculous instructions on foot, though, so you take a moment and work out the destination. Given that you can only walk on the street grid of the city, how far is the shortest path to the destination?

For example:

Following R2, L3 leaves you 2 blocks East and 3 blocks North, or 5 blocks away.
R2, R2, R2 leaves you 2 blocks due South of your starting position, which is 2 blocks away.
R5, L5, R5, R3 leaves you 12 blocks away.
 */

public class Day01 : IDay
{


	public long Part1()
	{
		PuzzleContext.Answer1 = 243;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.First().Split(", ").Select(Parse).ToArray();

		var direction = 0;

		int x = 0, y = 0;

		foreach (var (move, distance) in input)
		{
			if (move == 'L')
			{
				direction += 1;
			}
			else
			{
				direction -= 1;
			}
			direction = (direction + 4) % 4;

			switch (direction)
			{
				case 0:
					y += distance;
					break;
				case 1:
					x += distance;
					break;
				case 2:
					y -= distance;
					break;
				case 3:
					x -= distance;
					break;
			}


		}


		return Math.Abs(x) + Math.Abs(y);
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.First().Split(", ").Select(Parse).ToArray();

		var direction = 0;

		int x = 0, y = 0;

		var visited = new HashSet<string>();

		foreach (var (move, distance) in input)
		{
			if (move == 'L')
			{
				direction -= 1;
			}
			else
			{
				direction += 1;
			}
			direction %= 4;
			if (direction < 0) direction += 4;

			for (int i = 1; i <= distance; i++)
			{
				switch (direction)
				{
					case 0:
						x ++;
						break;
					case 1:
						y --;
						break;
					case 2:
						x --;
						break;
					case 3:
						y ++;
						break;
				}
				if (visited.Contains($"{x},{y}"))
					return Math.Abs(x) + Math.Abs(y);
				visited.Add($"{x},{y}");
			}
		}

		return -1;
	}

	public (char, int) Parse(string line)
	{
		var direction = line[0];
		var distance = int.Parse(line[1..]);
		return (direction, distance);
	}

}