using Common;

namespace ConsoleApp2025;

public class Day09 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 4745816424;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		var combinations = input.Combinations(2);

		var areas = combinations.Select(GetArea);

		return areas.Max();
	}

	private long GetArea((long x, long y)[] coordinates)
	{
		var minX = coordinates.Min(c => c.x);
		var maxX = coordinates.Max(c => c.x);
		var minY = coordinates.Min(c => c.y);
		var maxY = coordinates.Max(c => c.y);

		return ((maxX - minX)+1L) * ((maxY - minY) +1L);
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 1351617690;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToList();


		var edges = input.Zip(input.Skip(1)).ToList();
		edges.Add((input.Last(), input.First()));

		var combinations = input.Combinations(2);

	    var result = 0L;


		// This assumes that a rectangle outside the polygon is never larger than one on the inside... which is true for this input
		foreach (var c in combinations)
		{
			var (x1, y1) = c[0];
			var (x2, y2) = c[1];
			
			var area = GetArea(c);

			if (area <= result)
				continue;

			var hasIntersection = edges.Any(e => RectanglesOverlap(e.Item1, e.Item2, c[0], c[1]));

			if (!hasIntersection)
			{
				result = area;
			}
		}

		return result;
	}

	private bool RectanglesOverlap((long x, long y) r1Start, (long x, long y) r1End, 
                            (long x, long y) r2Start, (long x, long y) r2End)
	{
		var r1minX = Math.Min(r1Start.x, r1End.x);
		var r1maxX = Math.Max(r1Start.x, r1End.x);
		var r1minY = Math.Min(r1Start.y, r1End.y);
		var r1maxY = Math.Max(r1Start.y, r1End.y);

		var r2minX = Math.Min(r2Start.x, r2End.x);
		var r2maxX = Math.Max(r2Start.x, r2End.x);
		var r2minY = Math.Min(r2Start.y, r2End.y);
		var rrmaxY = Math.Max(r2Start.y, r2End.y);

		return r2maxX > r1minX && r2minX < r1maxX &&
			   rrmaxY > r1minY && r2minY < r1maxY;
	}

	private (long x, long y) Parse(string line)
	{
		var arr =  line.Split(',').Select(long.Parse).ToArray();
		return (arr[0], arr[1]);
	}

}