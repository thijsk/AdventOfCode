using Common;

namespace ConsoleApp2023;

public class Day06 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 0;
		PuzzleContext.UseExample = false;

		var input = Parse(PuzzleContext.Input);
		var races = input.distances.Length;

		long result = 1;

		for (int race = 0; race < races; race++)
		{
			var time = input.times[race];
			var distance = input.distances[race];

			var mintime = 0;
			var mindistance = 0; 
			while (mindistance <= distance)
			{
				mintime++;

				mindistance = (time - mintime) * mintime; ;
			}

			var maxtime = time;
			var maxdistance = 0;
			while (maxdistance <= distance)
			{
				maxtime--;
				maxdistance = (time - maxtime) * maxtime;
			}

			var options = 1 + (maxtime - mintime);
			result *= options;
		}

		return result;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = Parse(PuzzleContext.Input);
		var races = input.distances.Length;
		

		long result = 1;


		var time = long.Parse(string.Join("", input.times));
		var distance = long.Parse(string.Join("", input.distances));

		var mintime = 0L;
		var mindistance = 0L;
		while (mindistance <= distance)
		{
			mintime++;

			mindistance = (time - mintime) * mintime; ;
		}

		var maxtime = time;
		var maxdistance = 0L;
		while (maxdistance <= distance)
		{
			maxtime--;
			maxdistance = (time - maxtime) * maxtime;
		}

		var options = 1 + (maxtime - mintime);
		result *= options;
	

		return result;
	}

	public (int[] times, int[] distances) Parse(string[] lines)
	{
		var times = lines[0].Split(':')[1].Split<int>(' ');
		var distances = lines[1].Split(':')[1].Split<int>(' ');
		return (times, distances);
	}

}