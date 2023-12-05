using Common;

namespace ConsoleApp2023;

public class Day05 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 0;
		PuzzleContext.UseExample = false;

		var input = Parse(PuzzleContext.Input);

		long[] resources = input.seeds;
		foreach (var map in input.maps)
		{
			var newresources = new List<long>();
			foreach (var resource in resources)
			{
				bool mapped = false;
				foreach (var mapping in map.translate)
				{
					if (resource >= mapping.source && resource <= mapping.source + (mapping.count - 1))
					{
						var offset = resource - mapping.source;
						newresources.Add(mapping.dest + offset);
						mapped = true;
						break;
					}
				}
                if (!mapped)
                {
                    newresources.Add(resource);
                }
            }
			resources = newresources.ToArray();
		}

		return resources.Min();
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 0;
		PuzzleContext.UseExample = false;

		var input = Parse(PuzzleContext.Input);

		List<(long start, long count)> ranges = new();
		for (int i = 0; i < input.seeds.Length; i+=2)
		{
			ranges.Add((input.seeds[i], input.seeds[i+1]));
		}

		Queue<(long start, long count)> queue = new();
		foreach (var range in ranges)
		{
			queue.Enqueue(range);
		}

		foreach (var map in input.maps)
		{
			Console.WriteLine($"Queue count: {queue.Count}");
			Console.WriteLine("Using map: " + map.name);
			var newranges = new Queue<(long start, long end)>();
			while (queue.TryDequeue(out var range))
			{
				ConsoleX.Write("Mapping range: ");
				ConsoleX.WriteLine(WriteRange(range));
				bool mapped = false;
				foreach (var mapping in map.translate)
				{
					var mapstart = mapping.source;
					var mapend = mapping.source + (mapping.count - 1);
					var rangestart = range.start;
					var rangeend = range.start + (range.count - 1);

					if (mapstart > rangeend || mapend < rangestart)
						continue;

					ConsoleX.WriteLine($"Applying mapping {mapping.dest} {mapping.source} {mapping.count}");

					var start = Math.Max(mapstart, rangestart);
					var end = Math.Min(mapend, rangeend);
					ConsoleX.WriteLine($"Using subrange {start} - {end}");
					var newstart = mapping.dest + (start - mapstart);
					var newend = mapping.dest + (end - mapstart);

					var newrange = (newstart, newend - newstart + 1);
					ConsoleX.Write("Mapped to: ");
					ConsoleX.WriteLine(WriteRange(newrange));
					newranges.Enqueue(newrange);

					mapped = true;

					if (start > rangestart)
					{
						var newrangestart = rangestart;
						var newrangecount = start - rangestart;
						ConsoleX.Write("Adding start range: ");
						ConsoleX.WriteLine(WriteRange((newrangestart, newrangecount)));
						queue.Enqueue((newrangestart, newrangecount));
					}

					if (end < rangeend)
					{
						var newrangestart = end + 1;
						var newrangecount = rangeend - end;
						ConsoleX.Write("Adding end range: ");
						ConsoleX.WriteLine(WriteRange((newrangestart, newrangecount)));
						queue.Enqueue((newrangestart, newrangecount));
					}
				}
				if (!mapped)
				{
					newranges.Enqueue(range);
				}
			}
			queue = newranges;
			// probably should trim the queue here, combine ranges where possible
		}

		return queue.Select(r => r.start).Min();
	}

	private string WriteRange((long start, long count) range)
	{
		return $"{range.start} - {range.start + range.count - 1}";
	}

	public (long[] seeds, List<(string name, List<(long dest, long source, long count)> translate )> maps ) Parse(string[] lines)
	{
		(long[] seeds, List<(string name, List<(long dest, long source, long count)> translate)> maps) result = default;
		result.maps = new();

		var seeds = lines[0].Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Split<long>(' ');
		result.seeds = seeds;

		(string name, List<(long dest, long source, long count)> translate) map = default;
		foreach (var line in lines[1..])
		{
			if (line == string.Empty)
			{
				if (!string.IsNullOrEmpty(map.name))
					result.maps.Add(map);
				continue;
			}
				
			if (line.EndsWith(":"))
			{
				map.name = line;
				map.translate = new();
				continue;
			}

			(long dest, long source, long count) = line.Split<long>(' ');
			map.translate.Add((dest, source, count));

		}
		result.maps.Add(map);

		return result;
	}

}