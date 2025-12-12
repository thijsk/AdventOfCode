using Common;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace ConsoleApp2025;

public class Day12 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 499;
		PuzzleContext.UseExample = false;

		var (pieces, regions) = Parse(PuzzleContext.Input);

		var willFit = 0L;
		var wontFit = 0L;
		var mayFit = 0L;

		foreach (var region in regions)
		{
			var brutoSizeNeeded = region.Presents.Select((c, i) => c * pieces[i].BrutoArea).Sum();
			var nettoSizeNeeded = region.Presents.Select((c, i) => c * pieces[i].NettoArea).Sum();

			if (brutoSizeNeeded <= region.Area)
			{
				willFit++;
			}
			else if (nettoSizeNeeded > region.Area)
			{
				wontFit++;
			}
			else
			{
				mayFit++;
			}
		}

		// That is lucky, there are no regions in the "may fit" category
		Debug.Assert(mayFit == 0);
		Debug.Assert(willFit + wontFit + mayFit == regions.Count);

		return willFit;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		//var input = PuzzleContext.Input.Select(Parse).ToArray();

		return 0L;
	}

	private (List<Present> presents, List<Region> regions) Parse(string[] lines)
	{
		var parts = lines.SplitByEmptyLines();

		var rPresents = new List<Present>();
		var rRegions = new List<Region>();

		foreach (var part in parts)
		{
			if (part[0].EndsWith(":"))
			{
				// Present
				var present = new Present();
				present.Name = part[0].TrimEnd(':');
				present.Shape = part[1..].GetGrid();

				rPresents.Add(present);
			}
			else
			{
				// Regions part

				foreach (var rpart in part)
				{
					var region = new Region();
					var rparts = rpart.Split(":");
					var wxh = rparts[0].Split("x");
					region.Width = int.Parse(wxh[0]);
					region.Height = int.Parse(wxh[1]);
					region.Presents = rparts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToArray();
					rRegions.Add(region);
				}
			}
		}
		return (rPresents, rRegions);

	}



}

internal class Region
{
	public int Width { get; internal set; }
	public int Height { get; internal set; }
	public int[] Presents { get; internal set; }

	public int Area => Width * Height;
}

internal class Present
{
	public string Name { get; internal set; }
	public char[,] Shape { get; internal set; }

	public int BrutoArea
	{
		get
		{
			var area = Shape.GetLength(0) * Shape.GetLength(1);
			return area;
		}
	}

	public int NettoArea
	{
		get
		{
			var area = Shape.GetIndexes().Count(i => Shape[i.x, i.y] == '#');
			return area;
		}
	}
}