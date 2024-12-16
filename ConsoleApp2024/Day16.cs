using Common;
using Microsoft.Z3;

namespace ConsoleApp2024;

public class Day16 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 94436;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid();

		var startp = input.Find('S').First();
		var endp = input.Find('E').First();

		var start = (startp, Directions.Right);

		var result = input.Dijkstra<char, ((int x, int y) p, (int x, int y) d)>(start, GetNeighbors, s => s.p == endp);
		var resultp = result.path.ToDictionary(p => p.p);

		input.ToConsole((p, c) =>
		{
			if (resultp.TryGetValue(p, out var v))
			{
				if (v.d == Directions.Up)
				{
					ConsoleX.Write('^');
				} else if (v.d == Directions.Down)
				{
					ConsoleX.Write('v');
				}
				else if (v.d == Directions.Left)
				{
					ConsoleX.Write('<');
				}
				else if (v.d == Directions.Right)
				{
					ConsoleX.Write('>');
				}
			}
			else
				ConsoleX.Write(c);
		});

		return result.weight;
	}
	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid();

		var startp = input.Find('S').First();
		var endp = input.Find('E').First();

		var start = (startp, Directions.Right);

		var result = NotDijkstra<char, ((int x, int y) p, (int x, int y) d)>(input, start, GetNeighbors, s => s.p == endp);

		var resultp = result.Select(p => p.p).ToHashSet();

		input.ToConsole((p, c) =>
		{
			if ( resultp.Contains(p))
			{
				ConsoleX.Write('O');
			} else
				ConsoleX.Write(c);
		});

		return resultp.Count;
	}

	public static List<TStep> NotDijkstra<TCell, TStep>(TCell[,] grid, TStep start,
		Func<TCell[,], TStep, IEnumerable<(TStep point, long weight)>> getNeighbors, Func<TStep, bool> isGoal) where TStep : notnull
	{
		PriorityQueue<TStep, long> frontier = new();
		Dictionary<TStep, long> pathWeight = new();
		HashSet<TStep> visited = new();
		Dictionary<TStep, List<TStep>> moveMap = new();
		List<TStep> path = new();
		List<TStep> goals = new();

		frontier.Enqueue(start, 0);

		while (frontier.Count > 0)
		{
			//move
			var current = frontier.Dequeue();
			visited.Add(current);

			if (isGoal(current))
			{
				goals.Add(current);
			}

			// explore
			var neighbors = getNeighbors(grid, current);
			foreach (var neighbor in neighbors.Where(n => !visited.Contains(n.point)))
			{
				var currentWeigth = pathWeight.GetValueOrDefault(current, 0);
				var neighborPathWeight = currentWeigth + neighbor.weight;

				var oldNeighborWeight = pathWeight.GetValueOrDefault(neighbor.point, long.MaxValue);

				if (oldNeighborWeight > neighborPathWeight)
				{
					frontier.Enqueue(neighbor.point, neighborPathWeight);

					pathWeight.AddOrSet(neighbor.point, neighborPathWeight);
					moveMap.AddOrSet(neighbor.point, [current]);
				} else if (oldNeighborWeight == neighborPathWeight)
				{
					frontier.Enqueue(neighbor.point, neighborPathWeight);
					if (!moveMap[neighbor.point].Contains(current))
						moveMap[neighbor.point].Add(current);
				}
			}
		}

		// Backtrack
		Queue<TStep> allinPath = new();
		var minWeight = goals.Min(g => pathWeight[g]);
		foreach (var goal in goals)
		{
			var pw = pathWeight[goal];
			if (pw == minWeight)
				allinPath.Enqueue(goal);
		}
		while (allinPath.Count > 0)
		{
			var current = allinPath.Dequeue();
			path.Add(current);
			if (moveMap.TryGetValue(current, out var next))
			{
				foreach (var n in next)

				{
					if (!path.Contains(n))
						allinPath.Enqueue(n);
				}
			}
		}
		return path;
	}

	private IEnumerable<(((int x, int y) p, (int x, int y) d) point, long weight)> GetNeighbors(char[,] grid, ((int x, int y) p, (int x, int y) d) current)
	{
		var forwarddirection = current.d;
		var forward = grid.GetNeighborInDirection(current.p, forwarddirection);
			
		var leftdirection = current.d.TurnLeft();
		var left = grid.GetNeighborInDirection(current.p, leftdirection);

		var rightdirection = current.d.TurnRight();
		var right = grid.GetNeighborInDirection(current.p, rightdirection);

		if (forward.HasValue && grid[forward.Value.x, forward.Value.y] != '#')
		{
			yield return ((forward.Value, forwarddirection), 1);
		}

		if (left.HasValue && grid[left.Value.x, left.Value.y] != '#')
		{
			yield return ((left.Value, leftdirection), 1001);
		}

		if (right.HasValue && grid[right.Value.x, right.Value.y] != '#')
		{
			yield return ((right.Value, rightdirection), 1001);
		}
	}
}