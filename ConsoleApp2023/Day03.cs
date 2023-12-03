﻿using Common;
using System.Diagnostics;

namespace ConsoleApp2023;

public class Day03 : IDay
{
	public long Part1()
	{
		PuzzleContext.Answer1 = 553825;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid(c => c);

		long sum = 0;

		for (int x = 0; x < input.GetLength(0); x++)

		{
            for (int y = 0; y < input.GetLength(1); y++)
			{
				var nexty = y;
				var number = "";

				while (nexty < input.GetLength(1) && char.IsDigit(input[x, nexty]))
				{
					number += input[x, nexty];
					nexty++;
				}

				if (number.Length > 0)
				{
					var neighbors = Enumerable.Range(y, nexty - y).SelectMany(iy => input.GetAllNeighbors(x, iy)).Select(c => input[c.x, c.y]);

					// if any of the neighbors is not a digit and not a dot
					if (neighbors.Any(c => !char.IsDigit(c) && c != '.'))
					{
						sum += long.Parse(number);
						ConsoleX.ForegroundColor = ConsoleColor.Green;
					}
					else
					{
						ConsoleX.ForegroundColor = ConsoleColor.Red;
					}	
					ConsoleX.Write(number);
					ConsoleX.ResetColor();
					y = nexty -1 ;
				}
				else
				{
					ConsoleX.Write(input[x, y]);
					y = nexty;
				}
			
			}

			ConsoleX.WriteLine();
		}

		return sum;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 93994191;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input.GetGrid(c => c);

		long sum = 0;

		var gears = new Dictionary<(int x, int y), (long, bool)>();

		for (int x = 0; x < input.GetLength(0); x++)

		{
			for (int y = 0; y < input.GetLength(1); y++)
			{
				var nexty = y;
				var number = "";

				while (nexty < input.GetLength(1) && char.IsDigit(input[x, nexty]))
				{
					number += input[x, nexty];
					nexty++;
				}

				if (number.Length > 0)
				{
					var neighbors = Enumerable.Range(y, nexty - y).SelectMany(iy => input.GetAllNeighbors(x, iy)).Distinct().Select(c => (c.x, c.y, input[c.x, c.y]));

					// if any of the neighbors is not a digit and not a dot
					var ngears = neighbors.Where(n => n.Item3 == '*');

					foreach (var gear in ngears)
					{
						if (gears.ContainsKey((gear.x, gear.y)))
						{
							var firstvalue = gears[(gear.x, gear.y)].Item1;
							gears[(gear.x, gear.y)] = (firstvalue * long.Parse(number), true);
						}
						else 
							gears.Add((gear.x, gear.y), (long.Parse(number), false));
					}	

					if (ngears.Any())
					{
						ConsoleX.ForegroundColor = ConsoleColor.Green;
					}
					else
					{
						ConsoleX.ForegroundColor = ConsoleColor.Red;
					}
					ConsoleX.Write(number);
					ConsoleX.ResetColor();
					y = nexty - 1;
				}
				else
				{
					ConsoleX.Write(input[x, y]);
					y = nexty;
				}

			}

			ConsoleX.WriteLine();
		}

		return gears.Values.Where(v => v.Item2).Sum(v => v.Item1);
	}

	public long Parse(string line)
	{
		return long.Parse(line);
	}

}