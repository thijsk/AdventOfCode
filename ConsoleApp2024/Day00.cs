using Common;
using System.Diagnostics;

namespace ConsoleApp2024;

/// <summary>
/// https://aoc.infi.nl/2024
/// </summary>
public class Day00 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 4933;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		long sum = 0;

		for (int x = 0; x < 30; x++)
		{
			for (int y = 0; y < 30; y++)
			{
				for (int z = 0; z < 30; z++)
				{
					var result = RunProgram(input, x, y, z);
					sum += result;
				}
			}
		}

		return sum;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 13;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.Select(Parse).ToArray();

		long points = 0;
		var clouds = new List<List<(int x, int y, int z)>>();

		for (int x = 0; x < 30; x++)
		{
			for (int y = 0; y < 30; y++)
			{
				for (int z = 0; z < 30; z++)
				{
					var result = RunProgram(input, x, y, z);
					if (result > 0)
					{
						points++;
						var point = (x, y, z);
						var nclouds = clouds.Where(c => c.Any(p => IsNeighbor(p, point))).ToList();
						if (nclouds.Any())
						{
							var cloud = nclouds.First();
							cloud.Add(point);

							if (nclouds.Count > 1)
							{
								var npoints = nclouds.Skip(1).SelectMany(p => p);
								cloud.AddRange(npoints);
								;
								foreach (var c in nclouds.Skip(1))
								{
									clouds.Remove(c);
								}
							}
						}
						else
						{
							var cloud = new List<(int x, int y, int z)> {point};
							clouds.Add(cloud);
						}
					}
				}
			}
		}

		Debug.Assert(clouds.Sum(c => c.Count) == points);

		return clouds.Count;
	}

	private bool IsNeighbor((int x, int y, int z) first, (int x, int y, int z) second)
	{
		Debug.Assert(first != second);
		// Check if first is a neighbor of second in 3d space. They must share a face, so only one coordinate should differ by at most 1
		var diff = Math.Abs(first.x - second.x) + Math.Abs(first.y - second.y) + Math.Abs(first.z - second.z);

		return diff == 1;
	}

	private (string instruction, string value) Parse(string line)
	{
		var (instruction, value) = line.Split(' ');
		return (instruction, value);
	}

	private int RunProgram((string instruction, string value)[] input, int ix, int iy, int iz)
	{
		var counter = 0;
		var stack = new Stack<int>();

		while (true)
		{
			var instruction = input[counter].instruction;

			switch (instruction)
			{
				case "push":
					if (int.TryParse(input[counter].value, out var intvalue))
					{
						stack.Push(intvalue);
					}
					else
					{
						switch (input[counter].value.ToLower())
						{
							case "x":
								stack.Push(ix);
								break;
							case "y":
								stack.Push(iy);
								break;
							case "z":
								stack.Push(iz);
								break;
							default:
								throw new InvalidDataException();
						}
					}
					counter++;
					break;
				case "add":
					var a = stack.Pop();
					var b = stack.Pop();
					stack.Push(a + b);
					counter++;
					break;
				case "jmpos":
					var c = stack.Pop();
					if (c >= 0)
					{
						counter += int.Parse(input[counter].value);
					}
					counter++;
					break;
				case "ret":
					return stack.Pop();
				default:
					throw new InvalidDataException();
			}

		}
	}
}