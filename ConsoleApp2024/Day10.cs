using Common;

namespace ConsoleApp2024;

public class Day10 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 778;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid<int>(c => c == '.' ? -1 : int.Parse(c.ToString()));

		var trailheads = input.Find(0);

		return trailheads.Sum(h => CountTrails(input, h).Count);
	}


	public long Part2()
	{
		PuzzleContext.Answer2 = 1925;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid<int>(c => c == '.' ? -1 : int.Parse(c.ToString()));

		var trailheads = input.Find(0);

		return trailheads.Sum(h => CountTrails2(input, h).Count);
	}

	private HashSet<(int x, int y)> CountTrails(int[,] input, (int x, int y) start)
	{
		var value = input[start.x, start.y];

		if (value == 9)
		{
			return [start];
		}

		var result = new HashSet<(int x, int y)>();

		input.GetNeighbors(start.x, start.y).Where(n => input[n.x, n.y] == value + 1)
			.SelectMany(neighbor => CountTrails(input, neighbor))
			.ToList().ForEach(n => result.Add(n));

		return result;
	}

	private List<(int x, int y)> CountTrails2(int[,] input, (int x, int y) start)
	{
		var value = input[start.x, start.y];

		if (value == 9)
		{
			return [start];
		}

		var result = new List<(int x, int y)>();
		input.GetNeighbors(start.x, start.y).Where(n => input[n.x, n.y] == value + 1)
			.SelectMany(n => CountTrails2(input, n))
			.ToList().ForEach(result.Add);

		return result;
	}


}