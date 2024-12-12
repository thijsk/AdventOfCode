using Common;

namespace ConsoleApp2024;

public class Day12 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 1363682;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid<char>(c => c);
		return FindRegions(input).Sum(r => GetSize(input, r));
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 787680;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid(c => c);
		return FindRegions(input).Sum(r => GetSize2(input, r));
	}

	private long GetSize(char[,] input, HashSet<(int x, int y)> region)
	{
		var area = region.Count;
		var perimeter = region.Sum(f => 4 - input.GetNeighbors(f).Count(region.Contains));
		long size = area * perimeter;
		return size;
	}


	private long GetSize2(char[,] input, HashSet<(int x, int y)> region)
	{
		var area = region.Count;
		var sidenodes = region.Where(r => input.GetNeighbors(r).Count(region.Contains) < 4).ToHashSet();

		var ups = sidenodes.Where(s => IsEdge(input, region, s, Directions.Up)).ToHashSet();
		var downs = sidenodes.Where(s => IsEdge(input, region, s, Directions.Down)).ToHashSet();
		var lefts = sidenodes.Where(s => IsEdge(input, region, s, Directions.Left)).ToHashSet();
		var rights = sidenodes.Where(s => IsEdge(input, region, s, Directions.Right)).ToHashSet();

		var sides = new List<HashSet<(int x, int y)>>();

		sides.AddRange(SplitSides(ups, [Directions.Left, Directions.Right]));
		sides.AddRange(SplitSides(downs, [Directions.Left, Directions.Right]));
		sides.AddRange(SplitSides(lefts, [Directions.Up, Directions.Down]));
		sides.AddRange(SplitSides(rights, [Directions.Up, Directions.Down]));

		long size = area * sides.Count;
		return size;
	}

	private IEnumerable<HashSet<(int x, int y)>> SplitSides(HashSet<(int x, int y)> sides, (int x, int y)[] directions)
	{
		var result = new List<HashSet<(int x, int y)>>();

		HashSet<(int x, int y)> visited = [];

		foreach (var point in sides)
		{
			if (visited.Contains(point))
				continue;
			HashSet<(int x, int y)> currentSide = [ point ];

			foreach (var direction in directions)
			{
				var current = point;
				while (true)
				{
					var next = current.Add(direction);
					if (!sides.Contains(next))
						break;
					currentSide.Add(next);
					visited.Add(next);
					current = next;
				}
			}
			result.Add(currentSide);
		}
		return result;
	}

	private bool IsEdge(char[,] input, HashSet<(int x, int y)> region, (int x, int y) node, (int x, int y) direction)
	{
		var n = input.GetNeighborInDirection(node, direction);
		if (!n.HasValue)
			return true;
		return !region.Contains(n.Value);
	}

	private static List<HashSet<(int x, int y)>> FindRegions(char[,] input)
	{
		List<HashSet<(int x, int y)>> regions = [];
		HashSet<(int x, int y)> visited = [];

		foreach (var i in input.GetIndexes())
		{
			if (visited.Contains(i)) 
				continue;

			HashSet<(int x, int y)> region = [];
			Queue<(int x, int y)> queue = new();
			queue.Enqueue(i);
			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				if (!visited.Add(current))
					continue;
				region.Add(current);
				var neighbors = input.GetNeighbors(current).Where(n => input[current.x, current.y] == input[n.x, n.y]);
				foreach (var neighbor in neighbors)
				{
					queue.Enqueue(neighbor);
				}
			}
			regions.Add(region);
		}

		return regions;
	}
}