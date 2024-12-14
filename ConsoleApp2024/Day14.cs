using Common;
using System.Text.RegularExpressions;

namespace ConsoleApp2024;

public class Day14 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 224357412;
		PuzzleContext.UseExample = false;

		var xlimit = PuzzleContext.UseExample ? 11 : 101;
		var ylimit = PuzzleContext.UseExample ? 7 : 103;


		var input = PuzzleContext.Input.Select(Parse).ToArray();

		for (int i = 0; i < 100; i++)
		{
			foreach (var robot in input)
			{
				robot.Step(xlimit, ylimit);
			}
		}

		var midx = xlimit / 2;
		var midy = ylimit / 2;

		// q value = number of robots on each point multiplied together

		var q1 = input.Count(r => r.p.x < midx && r.p.y < midy);
		var q2 = input.Count(r => r.p.x > midx && r.p.y < midy);
		var q3 = input.Count(r => r.p.x < midx && r.p.y > midy);
		var q4 = input.Count(r => r.p.x > midx && r.p.y > midy);

		return q1 * q2 * q3 * q4;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 7083;
		PuzzleContext.UseExample = false;

		var xlimit = PuzzleContext.UseExample ? 11 : 101;
		var ylimit = PuzzleContext.UseExample ? 7 : 103;
		var midx = xlimit / 2;
		var midy = ylimit / 2;

		var input = PuzzleContext.Input.Select(Parse).ToArray();
		int step = 0;

		var pictures = new HashSet<int>();

		var maxNeighbors = 0;
		var bestStep = 0l;

		while (true)
		{
			step++;
			//input.AsParallel().ForAll(r => r.Step(xlimit, ylimit));
			Array.ForEach(input, r => r.Step(xlimit, ylimit));

			//if (!pictures.Add(input.Select(r => r.p).GetHashCodeOfList()))
			//{
			//	Console.WriteLine(step);
			//	break;
			//}

			var points = input.Select(r => r.p).ToHashSet();
			//var grid = GetGrid(points, xlimit, ylimit);
			//var neighborCount = points.Sum(i => grid[i.x, i.y] == '#' ? grid.GetAllNeighbors(i).Count(c => grid[c.x, c.y] == '#') : 0);
			//if (neighborCount <= maxNeighbors) continue;
			//maxNeighbors = neighborCount;
			//Print(xlimit, ylimit, points);
			//Console.WriteLine($"neighbors: {neighborCount} - step: {step}");
			//bestStep = step;

			if (points.Count() != input.Length)
				continue;
			
			Print(xlimit, ylimit, points);
			Console.WriteLine(step);
			return step;
		}
		return bestStep;
	}

	private char[,] GetGrid(HashSet<(int x, int y)> points, int xlimit, int ylimit)
	{
		var grid = new char[xlimit, ylimit];

		foreach (var p in points)
		{
			grid[p.x,p.y] = '#';
		}

		return grid;
	}

	private void Print(int xlimit, int ylimit, HashSet<(int x, int y)> points)
	{
		for (var y = 0; y < ylimit; y++)
		{
			for (var x = 0; x < xlimit; x++)
			{
				Console.Write(points.Contains((x, y)) ? "#" : " ");
			}
			Console.WriteLine();
		}
	}

	private Robot Parse(string line)
	{
		var regex = new Regex("^p=(.*),(.*) v=(.*),(.*)$");

		var match = regex.Match(line);

		return new Robot(
			(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)),
			(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value))
		);
	}

	class Robot((int x, int y) p, (int x, int y) v)
	{
		public (int x, int y) p = p;
		public (int x, int y) v = v;

		public void Step(int xlimit = 101, int ylimit = 103)
		{
			p.x += v.x;
			p.y += v.y;

			p.x = (p.x + xlimit) % xlimit;
			p.y = (p.y + ylimit) % ylimit;
		}
	}
}