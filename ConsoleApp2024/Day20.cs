using Common;
using System.Linq;

namespace ConsoleApp2024;

public class Day20 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 1197;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid();

		List<(int x, int y)> track = new();

		var start = input.Find('S').First();
		var finish= input.Find('E').First();

		var current = start;

		track.Add(current);

		while (current != finish)
		{
			var next = input.GetNeighbors(current).Except(track).Single(n => input[n.x, n.y] != '#');
			track.Add(next);
			current = next;
		}

		var UpUp = Directions.Up.Add(Directions.Up);
		var DownDown = Directions.Down.Add(Directions.Down);
		var LeftLeft = Directions.Left.Add(Directions.Left);
		var RightRight = Directions.Right.Add(Directions.Right);
		(int x, int y)[] doubleDirections = [UpUp, DownDown, LeftLeft, RightRight];

		var foundCheats = new List<int>();

		for (int i = 0; i < track.Count; i++)
		{
			var position = track[i];
			var time = i;
			var cheats = input.GetNeighborsInDirection(position, doubleDirections).Where(c => track.IndexOf(c) > time).ToList();
			if (cheats.Count > 0)
			{
				foreach (var cheat in cheats)
				{
					var cheatTime = track.IndexOf(cheat);
					var cheatDistance = cheatTime - time - 2;
					if (cheatDistance > 0)
						foundCheats.Add(cheatDistance);
				}
			}
		}

		return foundCheats.Count(c => c >= 100);
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 944910;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid();

		Dictionary<(int x, int y), int> track = new();
		List<(int x, int y)> route = new();

		var start = input.Find('S').First();
		var finish = input.Find('E').First();

		var current = start;

		track.Add(current, 0);
		route.Add(current);
		var t = 0;

		while (current != finish)
		{
			var next = input.GetNeighbors(current).Except(track.Keys).Single(n => input[n.x, n.y] != '#');
			track.Add(next, ++t);
			route.Add(next);
			current = next;
		}

		const int maxDuration = 20;
		const int minGain = 100;
		return route.AsParallel().Select((p, i) => (p,i)).Sum(t => route.Skip(t.i + minGain + 1).Select(p => (t.p.ManhattanDistance(p), track[p] - t.i))
			.Count(p => p is { Item1: <= maxDuration, Item2: > maxDuration } && (p.Item2 - p.Item1) >= minGain));
	}

	private static IEnumerable<((int x, int y) c, int duration)> FindCheats(char[,] input, (int x, int y) position, int maxduration, int currentduration, HashSet<((int x, int y), int d)> found)
	{
		if (!found.Add((position, currentduration))) yield break;
		var neighbors = input.GetNeighborsInDirection(position, Directions.AllCardinal);

		foreach (var neighbor in neighbors)
		{
			yield return (neighbor, currentduration);
		}
		foreach (var neighbor in neighbors) {
			if (currentduration >= maxduration) continue;
			foreach (var c in FindCheats(input, neighbor, maxduration, currentduration + 1, found))
			{
				yield return c;
			}
		}

	}
}