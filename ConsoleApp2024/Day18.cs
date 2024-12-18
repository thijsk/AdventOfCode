using Common;

namespace ConsoleApp2024;

public class Day18 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 246;
		PuzzleContext.UseExample = false;

		var size = PuzzleContext.UseExample ? 6 : 70;
		var falling = PuzzleContext.UseExample ? 12 : 1024;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var grid = new int[size + 1, size + 1];

		for (int i = 0; i < falling; i++)
		{
			grid[input[i].x, input[i].y] = i + 1;
		}

		//grid.ToConsole(i => i == 0 ? '.' : '#');

		var start = (0, 0);
		var goal = (size, size);

		var path = grid.Dijkstra(start, goal, (gr, c) =>
		{
			var n = gr.GetNeighbors(c).Where(n => gr[n.x, n.y] == 0).Select(n => (n, 1L));
			return n;
		});

		return path.Length;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 2958;
		PuzzleContext.UseExample = false;

		var size = PuzzleContext.UseExample ? 6 : 70;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var grid = new int[size + 1, size + 1];

		for (var i = 0; i < input.Length; i++)
		{
			grid[input[i].x, input[i].y] = i + 1;
		}

		var start = (0, 0);
		var goal = (size, size);

		for (var falling = input.Length -1; falling >= 0; falling--)
		{
			var path = grid.Dijkstra(start, goal, (gr, c) =>
			{
				var n = gr.GetNeighbors(c).Where(n => gr[n.x, n.y] == 0 || grid[n.x, n.y] > falling).Select(n => (n, 1L));
				return n;
			});

			if (path.Length <= 0) continue;
			Console.WriteLine($"({input[falling].y}, {input[falling].x})");
			return falling;
		}

		return 0;
	}

	private (int x, int y) Parse(string line)
	{
		(int x, int y) = line.Split(',').Select(int.Parse).ToArray();
		return (y, x);
	}

}