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

		var result = input.DijkstraAllShortestPathPoints<char, ((int x, int y) p, (int x, int y) d)>(start, GetNeighbors, s => s.p == endp);

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